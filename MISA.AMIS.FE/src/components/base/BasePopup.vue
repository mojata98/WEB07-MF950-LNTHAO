<template>
  <div
    class="popup-mask"
    :class="{ 'hidePopup': this.hidePopup }"
  >
    <div class="popup-container">
      <div class="popup-top">
        <div class="content">
          <div v-if="this.modePopup === 1" class="flex">
            <div class="popup-icon popup-icon-error"></div>
            <div class="popup-text">{{ this.message }} không được để trống.</div>
          </div>
          <div v-else-if="this.modePopup === 2" class="flex">
            <div class="popup-icon popup-icon-warning"></div>
            <div class="popup-text">Mã nhân viên bị trùng.</div>
          </div>
          <div v-else-if="this.modePopup === 3" class="flex">
            <div class="popup-icon popup-icon-warning"></div>
            <div class="popup-text">
              Bạn có chắc chắn muốn xóa nhân viên có mã nhân viên <b>{{ message }}</b> không?
            </div>
          </div>
          <div v-else class="flex">
            <div class="popup-icon popup-icon-information"></div>
            <div class="popup-text">
              Dữ liệu đã bị thay đổi. Bạn có muốn cất không?
            </div>
          </div>
        </div>
      </div>
      <div class="popup-bottom">
        <div v-if="this.modePopup === 1" class="flex">
          <button
            class="button button-add"
            style="margin-left: 154px; margin-right: 153px"
            @click="closePopup"
          >
            <b>Đóng</b>
          </button>
        </div>
        <div v-else-if="this.modePopup === 2" class="flex">
          <button
            class="button button-add"
            style="position: absolute; right: 30px"
            @click="closePopup"
          >
            <b>Đồng ý</b>
          </button>
        </div>
        <div v-else-if="this.modePopup === 3" class="flex">
          <button class="button button-cancel" @click="closePopup">
            <b>Không</b>
          </button>
          <button 
          style="position: absolute; right: 30px;"   
          class="button button-add" @click="deleteEmployee">
            <b>Có</b>
          </button>
        </div>
        <div v-else class="flex">
          <button class="button button-cancel" @click="closePopup">
            <b>Hủy</b>
          </button>
          <button
            class="button button-cancel"
            style="position: absolute; right: 100px"
            @click="closePopupAndEmployeeForm"
          >
            <b>Không</b>
          </button>
          <button 
          class="button button-add" 
          style="position: absolute; right: 30px;"  
          @click="closePopupAndSave">
            <b>Có</b>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import eventBus from '../../eventBus';
import { URL } from '../../resources/const';
export default {
  data() {
    return {
      // Trạng thái Popup:
      // 1 - Error Validate;
      // 2 - Warning trùng mã;
      // 3 - Warning xóa NV;
      // 4 - Infor dữ liệu bị thay đổi;
      modePopup: 1,
      message: "",  // Thông điệp cần gửi
      employee: {}, // Thông tin nhân viên nhận từ các component khác
      hidePopup: true, // ẩn Popup
    };
  },

  created() {
    /**
    * Nhận sự kiện xóa từ ContextMenu
    * CreatedBy: LNT (02/09)
    */
    eventBus.$on("deleteMode", (value) => {
        this.employee = value;
        this.modePopup = 3;
        this.message = value.EmployeeCode;
        this.hidePopup = false;
    });
    /**-----------------------------------------------------
     * Nhận sự kiện check dữ liệu bị trống từ EmployeeForm
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("invalidData", (value) =>{
        this.modePopup = 1;
        this.message = value;
        this.hidePopup = false;
    });
    /**-------------------------------------------
     * Nhận sự kiện check trùng mã từ EmployeeForm
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("duplicateEmployeeCode", () =>{
      this.modePopup = 2;
      this.hidePopup = false;
    });
    /**
     * Nhận sự kiện data bị thay đổi
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("updateData", () =>{
      this.modePopup = 4;
      this.hidePopup = false;
    });
    
  },

  methods: {
    /**------------------------
     * Đóng popup tại các mode
     * CreatedBy: LNT (02/09)
     */
    closePopup() {
      this.hidePopup = true;
    },
    /**--------------------------------------------------
     * Xóa nhân viên khi ấn nút CÓ tại modePopup = 3
     * CreatedBy: LNT (02/09)
     */
    async deleteEmployee(){
        var vm = this;
        await axios
        .delete(`${URL}/${vm.employee.EmployeeId}`)
        .then((res) => {
          this.$toast.success("Xóa thành công!", {
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
          console.log(res);
        })
        .catch((res) => {
          this.$toast.error("Xóa không thành công!", {
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
          console.log(res);
        });

        this.message = "";
        this.hidePopup = true;
        eventBus.$emit("reloadData");
    },
    /**
     * Đóng Popup và EmployeeForm
     * CreatedBy: LNT (02/09)
     */
    closePopupAndEmployeeForm(){
      this.hidePopup = true;
      eventBus.$emit("closeEmployeeForm");
      eventBus.$emit("resetForm");
    },
    /**
     * Tạo sự kiện lưu dữ liệu bị thay đổi
     * CreatedBy: LNT (02/09)
     */
    closePopupAndSave(){
      this.hidePopup = true;
      eventBus.$emit("closePopupAndSave");
    }
  },
};
</script>

<style scoped>
@import url(../../css/base/popup.css);

.hidePopup {
  display: none;
}

.flex {
  display: flex;
  align-items: center;
}
</style>