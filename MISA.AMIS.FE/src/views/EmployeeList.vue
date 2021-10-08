<template>
  <div class="content-body">
    <div class="content-scroll">
      <div class="content-title">
        <div class="title-text">Nhân viên</div>
        <button
          class="button button-add"
          id="AddNewEmployee"
          @click="openEmployeeForm"
        >
          Thêm mới nhân viên
        </button>
      </div>
      <div class="content-table">
        <div class="table-tool">
          <div class="input-search">
            <input
              style="width: 270px"
              type="text"
              placeholder="Tìm theo mã, tên nhân viên"
              v-model="keysearch"
              @keydown.enter="searchByKeysearch"
            />
            <div class="icon-search"></div>
          </div>
          <div class="icon-refresh" @click="reloadTableAndFilter"></div>
          <div class="icon-excell" @click="exportExcell"></div>
        </div>
        <BaseTable
          :headers="headers"
          :employees="employees"
          :pageSize="pageSize"
          @fixInfor="fixInfor"
        />
      </div>
      <div class="paginations">
        <div class="pagination-left">
          Tổng số: &nbsp; <b> {{ this.amountPage }} </b>&nbsp; bản ghi
        </div>
        <div class="pagination-right">
          <div class="combobox-paging">
            <BaseDropdown
              :data="dataPage"
              ref="textDropdownPaging"
              @get="getValPageSize"
              @setValueDefaultDropdown="setValueDefaultDropdown"
            />
          </div>
          <div class="paging">
            <Paginate
              :page-count="this.numPages"
              :page-range="3"
              :margin-pages="1"
              :click-handler="clickCallback"
              :prev-text="'Trước'"
              :next-text="'Sau'"
              :container-class="'paging'"
              :page-class="'page-index-number '"
              :prev-link-class="'previous-page'"
              :next-link-class="'next-page'"
            />
          </div>
        </div>
      </div>
    </div>
    <BaseLoading :isLoading="isLoading" />
    <EmployeeForm
      :hideForm="hideForm"
      @closeForm="closeForm"
      ref="modeForm"
      :modeParent="modeParent"
      @reloadTableAndFilter="reloadTableAndFilter"
      @departmentNull="departmentNull"
    />
  </div>
</template>

<script>
let headers = [
  { text: "Giới tính", align: "left", minwidth: "60px" },
  { text: "Ngày sinh", align: "center", minwidth: "50px" },
  { text: "Số CMND", align: "left", minwidth: "100px" },
  { text: "Chức danh", align: "left", minwidth: "100px" },
  { text: "Tên đơn vị", align: "left", minwidth: "150px" },
  { text: "Số tài khoản", align: "left", minwidth: "100px" },
  { text: "Tên ngân hàng", align: "right", minwidth: "100px" },
  { text: "Chi nhánh TK ngân hàng", align: "left", minwidth: "150px" },
];
import BaseTable from "./../components/base/BaseTable.vue";
import axios from "axios";
import BaseLoading from "../components/base/BaseLoading.vue";
import EmployeeForm from "../views/EmployeeForm.vue";
import BaseDropdown from "../components/base/BaseDropdown.vue";
import Paginate from "vuejs-paginate";
import { URL, MESSAGE } from "../resources/const";
import { STATUS_CODE } from "../resources/enum";
import eventBus from "../eventBus";

export default {
  data() {
    return {
      headers: headers, //tiêu đề bảng
      employee: {}, // thông tin 1 nhân viên
      modeParent: 0, // trạng thái: thêm(cất) - 0, sửa - 1, xóa - 2, cất và thêm - 4, nhân bản - 5
      pageSize: 10, // số lượng bản ghi trong 1 trang
      pageIndex: 1, // trang hiện tại
      amountPage: 0, // tổng số bản ghi
      numPages: 0, // tổng số trang
      keysearch: "", // từ khóa tìm kiếm (Mã, Tên, SĐT)
      isLoading: false, // ẩn loading
      employees: [], // mảng các object nhân viên
      employeeId: "", // id của nhân viên
      hideForm: true, // ẩn EmployeeForm, false - hiện, true - ẩn
      // data cho dropdown paging
      dataPage: [
        { Text: "10 bản ghi trên 1 trang", Value: 10 },
        { Text: "20 bản ghi trên 1 trang", Value: 20 },
        { Text: "30 bản ghi trên 1 trang", Value: 30 },
        { Text: "50 bản ghi trên 1 trang", Value: 50 },
        { Text: "100 bản ghi trên 1 trang", Value: 100 },
      ],
    };
  },

  components: {
    BaseTable,
    BaseLoading,
    EmployeeForm,
    BaseDropdown,
    Paginate,
  },

  created() {
    /**------------------------------------------------------------------------
     * Load dữ liệu lần đầu
     * CreatedBy: LNT (02/09)
     */
    this.getEmployeesByFilter(this.pageIndex, this.pageSize, this.keysearch);

    /**------------------------------------
     * Load lại dữ liệu khi xóa thành công
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("reloadData", () => {
      this.reloadTableAndFilter();
    });

    /**-----------------------------------------------------------
     * Nhận sự kiện mở EmployeeForm từ ContextMenu mode Nhân bản
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("openForm", () => {
      this.hideForm = false;
    });

    /**------------------------------------------------
     * Nhận sự kiện đóng EmployeeForm từ Popup mode 4
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("closeEmployeeForm", () => {
      this.hideForm = true;
    });
  },

  methods: {
    /*-----------------------------------------------------------------
     *Lấy ra danh sách nhân viên theo các tiêu chí và phân trang
     *CreateBy: LNT(17/08)
     */
    getEmployeesByFilter() {
      var self = this;
      self.isLoading = true;
      try {
        axios
          .get(
            `${URL}/filter?pageIndex=${self.pageIndex}&pageSize=${self.pageSize}&keysearch=${self.keysearch}`
          )
          .then((res) => {
            if (res.data.statusCode == STATUS_CODE.SUCCESS) {
              self.employees = res.data.data.Data;
              self.employees.forEach((employee) => {
                employee.Option = false;
              });
              self.amountPage = res.data.data.TotalRecord;
              self.numPages = res.data.data.TotalPage;
              setTimeout(() => (self.isLoading = false), 1000);
            } else {
              this.$toast.error(MESSAGE.EXCEPTION_MSG, {
                position: "bottom-right",
                timeout: 2000,
              });
            }
          })
          .catch(() => {
            this.$toast.error(MESSAGE.EXCEPTION_MSG, {
              position: "bottom-right",
              timeout: 2000,
            });
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**-------------------------------------------------------
     * Load lại dữ liệu
     * CreatedBy: LNT(28/08)
     */
    reloadTableAndFilter() {
      var self = this;
      self.pageIndex = 1;
      self.$refs.textDropdownPaging.setTextDefault();
      self.isLoading = true;
      try {
        axios
          .get(
            `${URL}/filter?pageIndex=${this.pageIndex}&pageSize=${this.pageSize}`
          )
          .then((res) => {
            if (res.data.statusCode == STATUS_CODE.SUCCESS) {
              self.employees = res.data.data.Data;
              self.amountPage = res.data.data.TotalRecord;
              self.numPages = res.data.data.TotalPage;
              self.keysearch = "";
              setTimeout(() => (self.isLoading = false), 1000);
            } else {
              this.$toast.error(MESSAGE.EXCEPTION_MSG, {
                position: "bottom-right",
                timeout: 2000,
              });
            }
          })
          .catch(() => {
            this.$toast.error(MESSAGE.EXCEPTION_MSG, {
              position: "bottom-right",
              timeout: 2000,
            });
          });
      } catch (error) {
        console.log(error);
      }
    },
    /**-----------------------------------------------------------
     * Tìm kiếm theo thanh search
     * CreateBy: LNT(30/07)
     */
    searchByKeysearch() {
      if (this.keysearch == "") {
        this.reloadTableAndFilter();
      } else {
        this.pageIndex = 1;
        this.getEmployeesByFilter(
          this.pageIndex,
          this.pageSize,
          this.keysearch
        );
      }
    },
    /**--------------------------------------------------
     * Đặt giá trị cho pageSize dành cho combobox
     * CreatedBy: LNT(31/08)
     */
    getValPageSize(valuePageSize) {
      this.pageSize = valuePageSize;
      this.getEmployeesByFilter(this.pageIndex, this.pageSize, this.keysearch);
    },
    /**------------------------------------
     * Reload số bản ghi dành cho combobox
     * CreatedBy: LNT (31/08)
     */
    setValueDefaultDropdown(value) {
      this.pageSize = value;
    },
    /**------------------------------------------------------------
     * Hàm nhận sự kiện mở form nhân viên khi ấn nút THÊM NHÂN VIÊN
     * CreatedBy: LNT (31/08)
     */
    openEmployeeForm() {
      this.hideForm = !this.hideForm;
      this.modeParent = 0;
      this.employee = {};
      this.$refs.modeForm.passDataFromParent(this.modeParent, "");
    },
    /**---------------------------------------------
     * Xem thông tin nhân viên khi nhấn đúp dòng
     * CreatedBy: LNTHAO (29/07)
     */
    fixInfor() {
      this.hideForm = false;
    },
    /**-----------------------------------
     * Tắt form Employee khi ấn nút HỦY
     * CreatedBy: LNT (31/08)
     */
    closeForm() {
      this.hideForm = true;
    },
    /** -------------------------------------
     * Set trạng thái input đơn vị trống
     * CreatedBy: LNT (01/09)
     */
    departmentNull() {
      this.employee.DepartmentId = "";
    },
    /**--------------------
     * Sự kiện chuyển trang
     * CreatedBy: LNT (01/09)
     */
    clickCallback(pageNum) {
      this.pageIndex = pageNum;
      this.getEmployeesByFilter(this.pageIndex, this.pageSize, this.keysearch);
    },
    /**--------------------------
     * Xuất dữ liệu ra file Excel
     * CreatedBy: LNT (03/09)
     */
    exportExcell() {
      var self = this;
      self.isLoading = true;
      try {
        axios
          .get(`${URL}/export`)
          .then((res) => {
            if (res.data.statusCode == STATUS_CODE.SUCCESS) {
              // download file excel về máy
              window.location = res.data.data;
              self.$toast.success(MESSAGE.EXPORT_DATA, {
                position: "bottom-right",
                timeout: 5000,
                closeOnClick: true,
                pauseOnFocusLoss: true,
                pauseOnHover: true,
                draggable: true,
                draggablePercent: 0.6,
                showCloseButtonOnHover: false,
                hideProgressBar: true,
                closeButton: "button",
                icon: true,
                rtl: false,
              });
              self.isLoading = false;
            } else {
              this.$toast.error(MESSAGE.EXCEPTION_MSG, {
                position: "bottom-right",
                timeout: 2000,
              });
            }
          })
          .catch(() => {
            this.$toast.error(MESSAGE.EXCEPTION_MSG, {
              position: "bottom-right",
              timeout: 2000,
            });
          });
      } catch (error) {
        console.log(error);
      }
    },
  },
};
</script>

<style scoped>
#AddNewEmployee {
  position: absolute;
  right: 0;
}
</style>