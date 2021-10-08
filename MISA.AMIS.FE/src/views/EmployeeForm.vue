<template>
  <div class="form-mask" :class="{ hideForm: hideForm }" ref="employeeForm">
    <div class="form-container">
      <div class="header">
        <div class="title">
          <div class="flex">
            <div class="flex-title">
              <b>Thông tin nhân viên</b>
            </div>
            <div class="flex-choice">
              <input type="checkbox" class="check" />
              <div class="flex-choice-text">Là khách hàng</div>
            </div>

            <div class="flex-choice">
              <input type="checkbox" class="check" />
              <div class="flex-choice-text">Là nhà cung cấp</div>
            </div>
          </div>
        </div>

        <div class="close">
          <div class="help"></div>
          <div class="exit" @click="exitForm"></div>
        </div>
      </div>
      <ValidationObserver ref="modal_form">
        <form class="body">
          <div class="body-scroll">
            <div class="body-top">
              <div class="top-left">
                <div class="line">
                  <div class="code m-editor" style="width: 149px">
                    <div class="m-title">
                      <b>Mã <font color="red">*</font></b>
                    </div>
                    <ValidationProvider
                      rules="required"
                      name="Mã nhân viên"
                      v-slot="{ errors }"
                    >
                      <input
                        type="text"
                        class="m-input"
                        tabindex="1"
                        id="txtEmployeeCode"
                        autofocus
                        v-model="employee.EmployeeCode"
                        ref="EmployeeCode"
                        :class="{
                          'border-error': errors.length > 0 ? true : false,
                        }"
                        :title="errors[0]"
                      />
                    </ValidationProvider>
                  </div>
                  <div class="name m-editor" style="width: calc(100% - 149px)">
                    <div class="m-title">
                      <b>Tên <font color="red">*</font></b>
                    </div>
                    <ValidationProvider
                      rules="required"
                      name="Tên nhân viên"
                      v-slot="{ errors }"
                    >
                      <input
                        type="text"
                        class="m-input"
                        tabindex="2"
                        id="txtFullName"
                        ref="FullName"
                        v-model="employee.FullName"
                        :class="{
                          'border-error': errors.length > 0 ? true : false,
                        }"
                        :title="errors[0]"
                      />
                    </ValidationProvider>
                  </div>
                </div>
                <div class="line">
                  <div class="department m-editor" style="width: 100%">
                    <div class="m-title">
                      <b>Đơn vị <font color="red">*</font></b>
                    </div>
                    <ValidationProvider
                      rules="required"
                      name="Đơn vị"
                      v-slot="{ errors }"
                    >
                      <BaseCombobox
                        tabindex="7"
                        style="
                          border-radius: 4px;
                          height: 32px;
                          outline-color: #2ca01c;
                        "
                        class="width-100"
                        :class="{
                          'border-error': errors.length > 0 ? true : false,
                        }"
                        @selectedDepartment="getValueDepartment"
                        :value="employee.DepartmentId"
                        :title="errors[0]"
                        ref="Department"
                      />
                    </ValidationProvider>
                  </div>
                </div>
                <div class="line">
                  <div class="position m-editor" style="width: 100%">
                    <div class="m-title"><b>Chức danh </b></div>
                    <input
                      type="text"
                      class="m-input"
                      tabindex="10"
                      v-model="employee.PositionName"
                    />
                  </div>
                </div>
              </div>
              <div class="top-right">
                <div class="line">
                  <div
                    class="dob m-editor"
                    style="width: 160px; padding-right: 6px"
                  >
                    <div class="m-title"><b>Ngày sinh</b></div>
                    <DatePicker
                      v-model="employee.DateOfBirth"
                      :format="'DD/MM/YYYY'"
                      :value-type="'YYYY-MM-DD'"
                      style="width: 100%; outline-color: #2ca01c"
                      tabindex="3"
                      placeholder="DD/MM/YYYY"
                      :disabled-date="(date) => date >= new Date()"
                    >
                      <input
                        type="date"
                        class="m-input"
                        placeholder="DD/MM/YYYY"
                      />
                    </DatePicker>
                  </div>
                  <div
                    class="gender m-editor"
                    style="width: calc(100% - 160px)"
                  >
                    <div class="m-title"><b>Giới tính </b></div>
                    <div class="gender-group">
                      <div tabindex="4" style="outline-color: #2ca01c">
                        <input
                          id="male"
                          name="gender"
                          type="radio"
                          v-model="employee.Gender"
                          value="1"
                          class="radio-gender"
                        />
                      </div>
                      <label for="male" class="radio-label">Nam</label>
                      <div tabindex="5" style="outline-color: #2ca01c">
                        <input
                          id="female"
                          name="gender"
                          type="radio"
                          v-model="employee.Gender"
                          value="2"
                          class="radio-gender"
                        />
                      </div>
                      <label for="female" class="radio-label">Nữ</label>
                      <div tabindex="6" style="outline-color: #2ca01c">
                        <input
                          id="other"
                          name="gender"
                          type="radio"
                          v-model="employee.Gender"
                          value="0"
                          class="radio-gender"
                        />
                      </div>
                      <label for="other" class="radio-label">Khác</label>
                    </div>
                  </div>
                </div>
                <div class="line">
                  <div
                    class="idCode m-editor"
                    style="width: calc(100% - 160px); padding-right: 6px"
                  >
                    <div class="m-title"><b>Số CMND</b></div>
                    <input
                      type="text"
                      class="m-input"
                      tabindex="8"
                      v-model="employee.IdentityNumber"
                    />
                  </div>
                  <div class="idDate m-editor" style="width: 160px">
                    <div class="m-title"><b>Ngày cấp </b></div>
                    <DatePicker
                      v-model="employee.IdentityDate"
                      :format="'DD/MM/YYYY'"
                      :value-type="'YYYY-MM-DD'"
                      style="width: 100%; outline-color: #2ca01c"
                      tabindex="9"
                      placeholder="DD/MM/YYYY"
                      :disabled-date="(date) => date >= new Date()"
                    >
                      <input
                        type="date"
                        class="m-input"
                        placeholder="DD/MM/YYYY"
                      />
                    </DatePicker>
                  </div>
                </div>
                <div class="line">
                  <div class="idPlace m-editor" style="width: 100%">
                    <div class="m-title"><b>Nơi cấp </b></div>
                    <input
                      type="text"
                      class="m-input"
                      tabindex="11"
                      v-model="employee.IdentityPlace"
                    />
                  </div>
                </div>
              </div>
            </div>
            <div class="body-bottom">
              <div class="line">
                <div class="address m-editor" style="width: 100%">
                  <div class="m-title"><b>Địa chỉ </b></div>
                  <input
                    type="text"
                    class="m-input"
                    tabindex="12"
                    v-model="employee.Address"
                  />
                </div>
              </div>
              <div class="line">
                <div
                  class="phoneNumber m-editor"
                  style="width: 203px; padding-right: 6px"
                >
                  <div class="m-title"><b>ĐT di động</b></div>
                  <input
                    type="text"
                    class="m-input"
                    tabindex="13"
                    v-model="employee.PhoneNumber"
                  />
                </div>
                <div
                  class="landlineNumber m-editor"
                  style="width: 203px; padding-right: 6px"
                >
                  <div class="m-title"><b>ĐT cố định</b></div>
                  <input
                    type="text"
                    class="m-input"
                    tabindex="14"
                    v-model="employee.LandlineNumber"
                  />
                </div>
                <div
                  class="email m-editor"
                  style="width: 203px; padding-right: 6px"
                >
                  <div class="m-title"><b>Email</b></div>
                  <input
                    type="text"
                    class="m-input"
                    tabindex="15"
                    v-model="employee.Email"
                  />
                </div>
              </div>
              <div class="line">
                <div
                  class="phoneNumber m-editor"
                  style="width: 203px; padding-right: 6px"
                >
                  <div class="m-title"><b>Tài khoản ngân hàng</b></div>
                  <input
                    type="text"
                    class="m-input"
                    tabindex="16"
                    v-model="employee.BankCode"
                  />
                </div>
                <div
                  class="landlineNumber m-editor"
                  style="width: 203px; padding-right: 6px"
                >
                  <div class="m-title"><b>Tên ngân hàng</b></div>
                  <input
                    type="text"
                    class="m-input"
                    tabindex="17"
                    v-model="employee.BankName"
                  />
                </div>
                <div
                  class="email m-editor"
                  style="width: 203px; padding-right: 6px"
                >
                  <div class="m-title"><b>Chi nhánh</b></div>
                  <input
                    type="text"
                    class="m-input"
                    tabindex="18"
                    v-model="employee.BankBranch"
                  />
                </div>
              </div>
            </div>
          </div>
        </form>
      </ValidationObserver>
      <div class="footer">
        <div class="footer-top"></div>
        <div class="footer-middle"></div>
        <div class="footer-bottom">
          <div class="footer-bottom-left">
            <button
              tabindex="19"
              class="button button-cancel"
              @click="exitForm"
              style="outline-color: #c7c7c7"
            >
              <b>Hủy</b>
            </button>
          </div>
          <div class="footer-bottom-right">
            <button
              tabindex="20"
              class="button button-cancel"
              style="margin-right: 10px; outline-color: #c7c7c7"
              @click="save"
            >
              <b>Cất</b>
            </button>
            <button
              tabindex="21"
              class="button button-add"
              style="outline-color: #c7c7c7"
              @click="saveAndCreate"
            >
              <b>Cất và Thêm</b>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ValidationObserver, ValidationProvider, extend } from "vee-validate";
import DatePicker from "vue2-datepicker";
import "vue2-datepicker/index.css";
import BaseCombobox from "../components/base/BaseCombobox.vue";
import axios from "axios";
import eventBus from "../eventBus";
import { URL, MESSAGE, TITLE_TEXT_POPUP } from "../resources/const";
import { EMPLOYEEFORM_STATE, STATUS_CODE } from "../resources/enum";
extend("required", {
  validate(value) {
    return {
      required: true,
      valid: ["", null, undefined].indexOf(value) === -1,
    };
  },
  computesRequired: true,
});

export default {
  data() {
    return {
      employee: {}, // Thông tin nhân viên
      employees: {}, // Mảng thông tin toàn bộ NV trong DB
      titleName: "", // Dữ liệu bị trống
      id: "", // ID gán để sửa
      modeChild: 0, // Mode ở form
      employeeOriginalAdd: {}, // Object ban đầu khi modeChild = 0
      employeeOriginalEdit: {}, // Object ban đầu khi modeChild = 1
      employeeId: "", // Id của nhân viên
      newEmployeeCode: "", // Mã NV mới
      notifications: [], // Mảng chứa lỗi
      isHiddenWarning: true, // Trạng thái popup warning
      textError: "", // Thông báo lỗi
    };
  },

  components: {
    ValidationProvider,
    DatePicker,
    ValidationObserver,
    // Dropdown,
    BaseCombobox,
  },

  props: {
    hideForm: {
      type: Boolean,
      default: true,
    },
  },

  created() {
    /**------------------------------
     * Lấy dữ liệu cho form từ API theo ID
     * Author: LNTHAO (29/07/2021)
     */
    eventBus.$on("fixInfor", (value) => {
      let vm = this;
      this.modeChild = EMPLOYEEFORM_STATE.EDIT;
      this.id = value;
      axios
        .get(`${URL}/${value}`)
        .then((res) => {
          vm.employee = res.data.data;
          console.log(vm.employee);
        })
        .catch((res) => {
          console.log(res);
        });
    });

    /**---------------------------------------------
     * Gửi thông tin nhân viên cần nhân bản lên Form
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("cloneEmployee", (value) => {
      this.employee = value;
      this.modeChild = EMPLOYEEFORM_STATE.ADD;
      this.autoNewEmployeeCode();
    });

    /**-------------------------------------
     * Nhận sự kiện lưu dữ liệu bị thay đổi
     * CreatedBy: LNT (02/09)
     */
    eventBus.$on("closePopupAndSave", () => {
      this.$refs.modal_form.validate().then(async (success) => {
        if (!success) {
          if (!this.employee.EmployeeCode) {
            this.titleName = TITLE_TEXT_POPUP.EMPLOYEECODE;
            eventBus.$emit("invalidData", this.titleName);
            return;
          } else if (!this.employee.FullName) {
            this.titleName = TITLE_TEXT_POPUP.FULLNAME;
            eventBus.$emit("invalidData", this.titleName);
            return;
          } else if (!this.employee.DepartmentId) {
            this.titleName = TITLE_TEXT_POPUP.DEPARTMENT;
            eventBus.$emit("invalidData", this.titleName);
            return;
          }
        }
        if (this.modeChild == EMPLOYEEFORM_STATE.ADD) {
          if ((await this.checkDuplicateEmployeeCode()) == 0) {
            await this.addNewEmployee();
            this.$emit("closeForm");
            this.$emit("reloadTableAndFilter");
            this.employee = {};
            this.$refs.modal_form.reset();
          } else {
            eventBus.$emit("duplicateEmployeeCode");
          }
        } else {
          if ((await this.checkDuplicateEmployeeCode()) == 0) {
            this.updateEmployee();
            this.$emit("closeForm");
            this.$emit("reloadTableAndFilter");
            this.$refs.modal_form.reset();
          } else {
            eventBus.$emit("duplicateEmployeeCode");
          }
        }
      });
    });
    /**
     * Nhận sự kiện resetForm từ popup mode 4
     * CreatedBy: LNT (03/09)
     */
    eventBus.$on("resetForm", () => {
      this.$refs.modal_form.reset();
    });
  },

  methods: {
    /**------------------------------------
     * Tạo sự kiện đóng form khi ấn nút HỦY
     * CreatedBy: LNT (31/08)
     */
    closeForm() {
      this.$emit("closeForm");
      this.$refs.modal_form.reset();
    },
    /**---------------------------------------------------
     * Gán value cho DepartmentId
     * CreatedBy : LNT(30/08/2021)
     */
    getValueDepartment(value) {
      this.employee.DepartmentId = value;
    },
    /**------------------------
     * Sự kiện nhấn nút CẤT
     * CreatedBy: LNT (31/08)
     */
    save() {
      this.$refs.modal_form.validate().then(async (success) => {
        if (!success) {
          if (!this.employee.EmployeeCode) {
            this.titleName = TITLE_TEXT_POPUP.EMPLOYEECODE;
            eventBus.$emit("invalidData", this.titleName);
            return;
          } else if (!this.employee.FullName) {
            this.titleName = TITLE_TEXT_POPUP.FULLNAME;
            eventBus.$emit("invalidData", this.titleName);
            return;
          } else if (!this.employee.DepartmentId) {
            this.titleName = TITLE_TEXT_POPUP.DEPARTMENT;
            eventBus.$emit("invalidData", this.titleName);
            return;
          }
        }
        if (this.modeChild == EMPLOYEEFORM_STATE.ADD) {
          if ((await this.checkDuplicateEmployeeCode()) == 0) {
            await this.addNewEmployee();
            this.$emit("closeForm");
            this.$emit("reloadTableAndFilter");
            this.employee = {};
            this.$refs.modal_form.reset();
          } else {
            eventBus.$emit("duplicateEmployeeCode");
          }
        } else {
          if ((await this.checkDuplicateEmployeeCode()) == 0) {
            this.updateEmployee();
            this.$emit("closeForm");
            this.$emit("reloadTableAndFilter");
            this.$refs.modal_form.reset();
          } else {
            eventBus.$emit("duplicateEmployeeCode");
          }
        }
      });
    },

    /**------------------------------------
     * Hàm bắt sự kiện ấn nút CẤT VÀ THÊM
     * CreatedBy: LNT (01/09)
     */
    async saveAndCreate() {
      if (this.modeChild == EMPLOYEEFORM_STATE.ADD) {
        this.$refs.modal_form.validate().then(async (success) => {
          if (!success) {
            if (!this.employee.EmployeeCode) {
              this.titleName = TITLE_TEXT_POPUP.EMPLOYEECODE;
              eventBus.$emit("invalidData", this.titleName);
              return;
            } else if (!this.employee.FullName) {
              this.titleName = TITLE_TEXT_POPUP.FULLNAME;
              eventBus.$emit("invalidData", this.titleName);
              return;
            } else if (!this.employee.DepartmentId) {
              this.titleName = TITLE_TEXT_POPUP.DEPARTMENT;
              eventBus.$emit("invalidData", this.titleName);
              return;
            }
          }
          if ((await this.checkDuplicateEmployeeCode()) == 0) {
            await this.addNewEmployee();
            this.employee = {};
            this.$refs.modal_form.reset();
            this.employee.Gender = 1;
            this.autoNewEmployeeCode();
          } else {
            eventBus.$emit("duplicateEmployeeCode");
          }
        });
      } else {
        this.$refs.modal_form.validate().then(async (success) => {
          if (!success) {
            if (!this.employee.EmployeeCode) {
              this.titleName = TITLE_TEXT_POPUP.EMPLOYEECODE;
              eventBus.$emit("invalidData", this.titleName);
              return;
            } else if (!this.employee.FullName) {
              this.titleName = TITLE_TEXT_POPUP.FULLNAME;
              eventBus.$emit("invalidData", this.titleName);
              return;
            } else if (!this.employee.DepartmentId) {
              this.titleName = TITLE_TEXT_POPUP.DEPARTMENT;
              eventBus.$emit("invalidData", this.titleName);
              return;
            }
          }
          if ((await this.checkDuplicateEmployeeCode()) == 0) {
            await this.updateEmployee();
            this.modeChild = 0;
            this.employee = {};
            this.$refs.modal_form.reset();
            this.employee.Gender = 1;
            this.autoNewEmployeeCode();
          } else {
            eventBus.$emit("duplicateEmployeeCode");
          }
        });
      }
    },

    /**--------------------------
     * Nhận sự kiên đóng Popup
     * CreatedBy: LNT (31/08)
     */
    closePopup() {
      this.hidePopup = true;
    },

    /**-----------------
     * Hiển thị Popup
     * CreatedBy: LNT (01/09)
     */
    showPopup() {
      this.hidePopup = false;
    },

    /**------------------
     * Sự kiện ấn nút X
     * CreatedBy: LNT (01/09)
     */
    exitForm() {
      if (this.modeChild == 0) {
        if (
          JSON.stringify(Object.values(this.employeeOriginalAdd)) ===
          JSON.stringify(Object.values(this.employee))
        ) {
          this.$emit("closeForm");
          this.$refs.modal_form.reset();
        } else {
          eventBus.$emit("updateData");
        }
      } else {
        if (
          JSON.stringify(Object.values(this.employeeOriginalEdit)) ===
          JSON.stringify(Object.values(this.employee))
        ) {
          this.$emit("closeForm");
          this.$refs.modal_form.reset();
        } else {
          eventBus.$emit("updateData");
        }
      }
    },
    /**---------------------------------
     * Hàm nhận dữ liệu truyền từ parent
     * CreatedBy: LNT (01/09)
     */
    passDataFromParent(data, dataId) {
      this.modeChild = data;
      this.employeeId = dataId;
      if (data == 0) {
        this.employee = {};
        this.employee.Gender = 1;
        this.autoNewEmployeeCode();
      } else {
        this.bindDataToForm();
      }
    },

    /**--------------------------------------------
     * Điền thông tin từ database vào form theo ID
     * CreateBy: LNT(01/09)
     */
    async bindDataToForm() {
      var self = this;
      // call api
      try {
        await axios
          .get(URL + `/${self.employeeId}`)
          .then((res) => {
            if (res.data.statusCode == STATUS_CODE.SUCCESS) {
              self.employee = res.data;
              Object.assign(this.employeeOriginalEdit, this.employee);
            } else {
              this.$toast.error(MESSAGE.EXCEPTION_MSG, {
                position: "bottom-right",
                timeout: 2000,
              });
            }
          })
          .catch((error) => {
            console.log(error);
          });
      } catch (error) {
        console.log(error);
      }
      self.$nextTick(() => self.$refs.EmployeeCode.focus());
    },

    /**-----------------------------------------------------------------
     * Hàm sinh tự động mã nhân viên
     * CreateBy: LNT (28/08/2021)
     */
    async autoNewEmployeeCode() {
      this.$nextTick(() => this.$refs.EmployeeCode.focus());
      let self = this;
      try {
        await axios
          .get(URL + `/newEmployeeCode`)
          .then((res) => {
            if (res.data.statusCode == STATUS_CODE.SUCCESS) {
              self.$set(self.employee, "EmployeeCode", res.data.data);
              self.employeeOriginalAdd.Gender = 1;
              self.employeeOriginalAdd.EmployeeCode = res.data.data;
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

    /**-------------------------
     * Hàm thêm mới nhân viên
     * CreatedBy: LNT (01/09)
     */
    async addNewEmployee() {
      var vm = this;
      try {
        await axios
          .post(URL, vm.employee)
          .then((res) => {
            if (res.data.statusCode == STATUS_CODE.CREATED) {
              this.$toast.success(MESSAGE.ADD_MSG_SUCCESS, {
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
            } else if (res.data.statusCode == STATUS_CODE.BAD_REQUEST) {
              if (!this.employee.EmployeeCode) {
                this.titleName = TITLE_TEXT_POPUP.EMPLOYEECODE;
                eventBus.$emit("invalidData", this.titleName);
                return;
              } else if (!this.employee.FullName) {
                this.titleName = TITLE_TEXT_POPUP.FULLNAME;
                eventBus.$emit("invalidData", this.titleName);
                return;
              } else if (!this.employee.DepartmentId) {
                this.titleName = TITLE_TEXT_POPUP.DEPARTMENT;
                eventBus.$emit("invalidData", this.titleName);
                return;
              }
            } else {
              this.$toast.error(MESSAGE.EXCEPTION_MSG, {
                position: "bottom-right",
                timeout: 2000,
              });
            }
          })
          .catch((res) => {
            this.$toast.error(MESSAGE.EXCEPTION_MSG, {
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
      } catch (error) {
        console.log(error);
      }

      this.$emit("reloadTableAndFilter");
    },

    /**------------------------------
     * Hàm sửa thông tin nhân viên
     * CreatedBy: LNT (01/09)
     */
    async updateEmployee() {
      let vm = this;
      try {
        await axios
          .put(`${URL}/${vm.id}`, vm.employee)
          .then((res) => {
            if (res.data.statusCode == STATUS_CODE.SUCCESS) {
              this.$toast.success(MESSAGE.EDIT_MSG_SUCCESS, {
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
      this.$emit("reloadTableAndFilter");
    },
    /**---------------------------
     * Check trùng mã nhân viên - Trả về 1: Trùng mã; Trả về 0: Không trùng mã
     * CreatedBy: LNT (02/09)
     */
    async checkDuplicateEmployeeCode() {
      var self = this;
      var result = 0;
      try {
        await axios
          .get(URL)
          .then((res) => {
            if (res.data.statusCode == STATUS_CODE.SUCCESS) {
              self.employees = res.data.data;
              self.employees.forEach((item) => {
                if (
                  self.employee.EmployeeCode == item.EmployeeCode &&
                  self.employee.EmployeeId != item.EmployeeId
                ) {
                  result = result + 1;
                }
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
      return result;
    },
  },
};
</script>

<style scoped>
@import url(../css/layout/employeeForm.css);
@import url(../css/base/button.css);

.hideForm {
  display: none;
}

.border-error {
  border-color: red;
  outline-color: red;
}
</style>