<template>
  <div class="table-contain">
    <div class="table-content">
      <table class="base-table">
        <thead>
          <tr>
            <th
              style="left: 0; width: 50px; border-right: 1px solid #c7c7c7"
              class="sticky-cell-header"
            >
              <input class="checkbox checkbox-active" type="checkbox" />
            </th>
            <th
              style="
                left: 46px;
                width: 100px;
                padding-left: 10px;
                padding-right: 10px;
                border-right: 1px solid #c7c7c7;
              "
              class="sticky-cell-header float-left"
            >
              Mã nhân viên
            </th>
            <th
              style="
                left: 182px;
                width: 150px;
                padding-left: 10px;
                padding-right: 10px;
              "
              class="sticky-cell-header float-left"
            >
              Tên nhân viên
            </th>
            <th
              v-for="(item, index) in headers"
              :key="index"
              :style="{ 'text-align': item.align, 'min-width': item.minwidth }"
            >
              {{ item.text }}
            </th>
            <th
              style="right: 0; min-width: 100px; border-right: none"
              class="sticky-cell-header float-right"
            >
              Chức năng
            </th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(employee, index) in employees" :key="employee.EmployeeId"
              @dblclick="fixInfor(employee.EmployeeId)"
          >
            <!--Cột 1: Checkbox-->
            <td
              class="sticky-cell-body float-left"
              style="
                left: 0;
                width: 50px;
                border-right: 1px solid #c7c7c7;
                padding-top: 0;
              "
            >
              <input type="checkbox" class="checkbox" />
            </td>
            <!--Cột 2: Mã nhân viên-->
            <td
              style="
                left: 46px;
                width: 100px;
                padding-left: 10px;
                padding-right: 10px;
                border-right: 1px solid #c7c7c7;
                padding-top: 0;
              "
              class="sticky-cell-body float-left"
            >
              {{ employee.EmployeeCode }}
            </td>
            <!--Cột 3: Tên nhân viên-->
            <td
              style="
                left: 182px;
                width: 100px;
                padding-left: 10px;
                padding-right: 10px;
                padding-top: 0;
              "
              class="sticky-cell-body float-left"
            >
              {{ employee.FullName }}
            </td>
            <!--Cột 4: Giới tính-->
            <td>
              {{ formatGender(employee.Gender) }}
            </td>
            <!--Cột 5: Ngày sinh-->
            <td>
              {{ formatDate(employee.DateOfBirth) }}
            </td>
            <!--Cột 6: Số CMND-->
            <td>
              {{ employee.IdentityNumber }}
            </td>
            <!--Cột 7: Chức danh-->
            <td>
              {{ employee.PositionName }}
            </td>
            <!--Cột 8: Tên đơn vị-->
            <td>
              {{ employee.DepartmentName }}
            </td>
            <!--Cột 9: Số tài khoản-->
            <td>
              {{ employee.BankCode }}
            </td>
            <!--Cột 10: Tên ngân hàng-->
            <td>
              {{ employee.BankName }}
            </td>
            <!--Cột 11: Chi nhánh TK ngân hàng-->
            <td style="border-right: none">
              {{ employee.BankBranch }}
            </td>
            <!--Cột 12: Chức năng-->
            <td class="sticky-cell-body float-right">
              <div class="option-wrapper" style="display: flex">
                <div 
                  class="btn-edit" 
                  @click="fixInfor(employee.EmployeeId)"
                  style="cursor: pointer;" 
                >
                  Sửa
                </div>
                <div
                  class="box-option border-box"
                  @click="getCoordinate(index, employee, pageSize)"
                >
                  <div
                    class="btn-option"
                  ></div>
                 </div> 
              </div>
            </td>
          </tr>
          <tr v-if="employees.length == 0 ? true : false">
            <td colspan="12" style="text-align: center">
              Không có dữ liệu hiển thị.
            </td>
          </tr>
        </tbody>
      </table>
      <ContextMenu/>
    </div>
  </div>
</template>

<script>
import moment from "moment";
import eventBus from '../../eventBus';
import ContextMenu from '../employee/ContextMenu.vue';
export default {
  components:{
    ContextMenu,
  },

  data() {
    return {
      employeeId: "",
      isHiddenOption: false,
      selectedRow: -1,// hàng đang được chọn
      passToContextMenu: {// trạng thái của context menu
        index: 0,
        employeeObject: {},
        pageSize: 10,
      },
    };
  },

  props: {
    headers: {
      type: Array,
    },
    employees: {},
    mode: {
      type: Number,
      default: 0,
    },
    pageSize: {
      type: Number,
      default: 10,
    },
  },

  methods: {
    /**---------------------------------
     * Hàm format về text của giới tính
     * CreateBy:LNT(23/8/2021)
     */
    formatGender(value) {
      switch (value) {
        case 2:
          return "Nữ";
        case 1:
          return "Nam";
        case 0:
          return "Khác";
      }
    },

    /**---------------------------------------------------
     * Hàm format ngày tháng năm trên table
     * CreateBy:LNT(23/8/2021)
     */
    formatDate(value) {
      if (value) {
        return moment(String(value)).format("DD/MM/YYYY");
      }
    },

    /**------------------------------------------------
     * Hàm bắt sự kiện double click vào từng dòng trong table
     * CreateBy: LNT (23/08/2021)
     */
    fixInfor(employeeId) {
      this.employeeId = employeeId;
      this.$emit("fixInfor");
      eventBus.$emit("fixInfor", employeeId);
    },

    /**------------------------------------------------------------
     * Truyền thông tin index, id, pageSize cho action-container
     * CreatedBy: LNT (01/09)
     */
    getCoordinate(index, employee, pageSize){
      this.isHiddenOption = !this.isHiddenOption;
      this.passToContextMenu.index = index + 1;
      this.passToContextMenu.employeeObject = employee;
      this.passToContextMenu.pageSize = pageSize;
      eventBus.$emit("passDataToContextMenu", this.passToContextMenu);
    },
  },
};
</script>

<style scoped>
@import url(../../css/employee/list.css);


</style>