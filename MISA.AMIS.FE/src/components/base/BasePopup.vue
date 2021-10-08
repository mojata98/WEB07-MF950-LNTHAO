<template>
  <div class="popup-mask" :class="{ hidePopup: this.hidePopup }">
    <div class="popup-container">
      <div class="popup-top">
        <div class="content">
          <div v-if="this.modePopup === 1" class="flex">
            <div class="popup-icon popup-icon-error"></div>
            <div class="popup-text">
              {{ this.message }} không được để trống.
            </div>
          </div>
          <div v-else-if="this.modePopup === 2" class="flex">
            <div class="popup-icon popup-icon-warning"></div>
            <div class="popup-text">{{ this.message }}</div>
          </div>
          <div v-else-if="this.modePopup === 3" class="flex">
            <div class="popup-icon popup-icon-warning"></div>
            <div class="popup-text">
              Bạn có muốn xóa nhân viên có mã <b>{{ this.message }}</b> không?
            </div>
          </div>
          <div v-else class="flex">
            <div class="popup-icon popup-icon-information"></div>
            <div class="popup-text">
              {{ this.message }}
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
            style="position: absolute; right: 30px"
            class="button button-add"
            @click="deleteEmployee"
          >
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
            style="position: absolute; right: 30px"
            @click="closePopupAndSave"
          >
            <b>Có</b>
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
import eventBus from "../../eventBus";
import { URL, MESSAGE } from "../../resources/const";
import { POPUP_STATE, STATUS_CODE } from "../../resources/enum";
export default {
  data() {
    return {
      // Trạng thái Popup:
      // 1 - Error Validate;
      // 2 - Warning trùng mã;
      // 3 - Warning xóa NV;
      // 4 - Infor dữ liệu bị thay đổi;
      modePopup: 1,
      message: "", // Thông điệp cần gửi
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
      this.modePopup = POPUP_STATE.DELETE;
      this.message = value.EmployeeCode;
      this.hidePopup = false;
    });
    /**-----------------------------------------------------
     * Nhận sự kiện check dữ liệu bị trống từ EmployeeForm
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("invalidData", (value) => {
      this.modePopup = POPUP_STATE.VALIDATE;
      this.message = value;
      this.hidePopup = false;
    });
    /**-------------------------------------------
     * Nhận sự kiện check trùng mã từ EmployeeForm
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("duplicateEmployeeCode", () => {
      this.modePopup = POPUP_STATE.DUPLICATE_EMPLOYEECODE;
      this.hidePopup = false;
      this.message = MESSAGE.DUPLICATED_DATA;
    });
    /**
     * Nhận sự kiện data bị thay đổi
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("updateData", () => {
      this.modePopup = POPUP_STATE.UPDATE_DATA;
      this.message = MESSAGE.FORM_CHANGED;
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
    async deleteEmployee() {
      var vm = this;
      try {
        await axios
          .delete(`${URL}/${vm.employee.EmployeeId}`)
          .then((res) => {
            if (res.data.statusCode == STATUS_CODE.SUCCESS) {
              this.$toast.success(MESSAGE.DELETE_MSG_SUCCESS, {
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
            } else if (res.data.statusCode == STATUS_CODE.NOT_FOUND) {
              this.$toast.warning(MESSAGE.DATA_EMPTY, {
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

      this.message = "";
      this.hidePopup = true;
      eventBus.$emit("reloadData");
    },
    /**
     * Đóng Popup và EmployeeForm
     * CreatedBy: LNT (02/09)
     */
    closePopupAndEmployeeForm() {
      this.hidePopup = true;
      eventBus.$emit("closeEmployeeForm");
      eventBus.$emit("resetForm");
    },
    /**
     * Tạo sự kiện lưu dữ liệu bị thay đổi
     * CreatedBy: LNT (02/09)
     */
    closePopupAndSave() {
      this.hidePopup = true;
      eventBus.$emit("closePopupAndSave");
    },
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