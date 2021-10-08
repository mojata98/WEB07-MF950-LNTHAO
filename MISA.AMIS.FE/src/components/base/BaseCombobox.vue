<template>
  <div class="combobox">
    <ValidationObserver ref="combobox">
      <ValidationProvider
        rules="required"
        name="Đơn vị"
        v-slot="{ errors }"
      >
      <input type="text" class="width-100" v-model="keysearch "  tabindex="7"
      :class="{'border-error': errors.length > 0 ? true : false,}"
      :title="errors[0]"/>
      </ValidationProvider>  
      </ValidationObserver>
      <div
        class="combobox-icon"
        :class="{ rotate: !isHiddenCombobox }"
        @click="toggleCombobox"
      >
        <div class="btn-combobox"></div>
      </div>
    <div class="combobox-data" :class="{ 'hidden-combobox': isHiddenCombobox }">
      <table>
        <thead>
          <tr>
            <th>Mã đơn vị</th>
            <th>Tên đơn vị</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="(department, index) in departments"
            :key="index"
            @click="selectDepartment(index)"
            :class="{
              'combobox-active':
                currentDepartmentId == department.DepartmentId ? true : false,
            }"
          >
            <td>{{ department.DepartmentCode }}</td>
            <td>{{ department.DepartmentName }}</td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import { ValidationObserver, ValidationProvider, extend } from "vee-validate";
import axios from "axios";
import { URL_DEPARTMENT } from '../../resources/const';

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
  name: "Combobox",
  props: ["value"],
  component: {
    ValidationProvider,
    ValidationObserver,
  },
  data() {
    return {
      isHiddenCombobox: true,// trạng thái của combobox
      departments: [],// data departments động
      options: [],// data departments tĩnh
      keysearch: null,// text input
      currentDepartmentId: "",
      isSearching: true,
    };
  },
  created() {
    /**--------------------------------
     * Lấy dữ liệu đơn vị từ database
     * CreatedBy: LNT (02/09)
     */
     this.getDepartment();
  },
  mounted() {
    /**------------------------------------------
     * Bắt sự kiện click bất kỳ ra ngoài combobox
     * CreatedBy: LNT (02/09)
     */
    document.addEventListener("click", this.close);
  },
  watch: {
    keysearch() {
      console.log(this.departments);
      console.log(this.options);
      this.searchByKeysearch();
    },
    value()
    {
     this.setValueDepartment();
    }
  },
  methods: {
    /*----------------------------------------------------
     * Bắt sự kiện click vào combobox
     * CreatedBy: LNT(30/08/2021)
     */
    toggleCombobox() {
      this.isHiddenCombobox = !this.isHiddenCombobox;
      this.getDepartment();
    },

    /**-------------------------------------------------------------
     * Lấy ra tất cả đơn vị
     * CreatedBy:LNT(30/08/2021)
     */
    getDepartment() {
      var self = this;
      axios.get(URL_DEPARTMENT).then((res) => {
        self.departments = res.data.data;
        self.options = res.data.data;
      });
    },

    /**----------------------------------------------------------------------
     * Select department
     * CreatedBy: LNT(31/08/2021)
     */
    selectDepartment(index) {
      this.currentDepartmentId = this.options[index].DepartmentId;
      this.keysearch = this.options[index].DepartmentName;
      this.$emit("selectedDepartment", this.currentDepartmentId);
      this.isSearching = false;
    },

    /**-----------------------------------------------------------------------
     * Search theo keysearch
     * CreatedBy: LNT(31/08/2021)
     */
    searchByKeysearch() {
      if (this.isSearching) {
        // mở combobox
        this.isHiddenCombobox = false;
        this.isRotate = true;
      } else {
        // đóng combobox
        this.isHiddenCombobox = true;
        this.isRotate = false;
      }
      if (this.keysearch != null && this.keysearch.length > 0) {
        this.departments = [];
        this.options.forEach((element) => {
          if (
            element.DepartmentName.toLowerCase().includes(
              this.keysearch.toLowerCase()
            ) ||
            element.DepartmentCode.toLowerCase().includes(
              this.keysearch.toLowerCase()
            )
          ) {
            this.departments.push(element);
          }
        });
        this.isSearching = true;
      } else {
        this.currentDepartmentId = null;
        this.keysearch = null;
        this.departments = this.options;
        this.isSearching = true;
      }
    },

    /**---------------------------------------------------------
     * Bind data departmentName lên input
     * CreatedBy: LNT(31/08/2021)
     */
    setValueDepartment() {
      let flag = false;
      this.options.forEach((element) => {
        if (element.DepartmentId == this.value) {
          this.currentDepartmentId = this.value;
          this.keysearch = element.DepartmentName;
          flag = true;
        }
      });
      if (!flag) {
        this.currentDepartmentId = null;
        this.keysearch = null;
      }
      this.isSearching = false;
    },

    /**----------------------------------------------------------------------
     * Hàm check event khi click ra bên ngoài combobox
     * CreatedBy: LNT(28/08/2021)
     */
    close(e) {
      if (!this.$el.contains(e.target)) {
        if(this.currentDepartmentId != null)
        {
          let flag = false;
          this.options.forEach(element => {
            if(element.DepartmentId == this.currentDepartmentId)
            {
              this.keysearch = element.DepartmentName;
              flag = true;
            }
          });
          if(!flag)
          {
            this.currentDepartmentId = null;
            this.keysearch = null;
          }
        }
        else
        {
          this.keysearch = null;
        }
        this.isHiddenCombobox = true;
      }
    },
  },
};
</script>

<style scoped>
@import url(../../css/base/combobox.css);

.border-error{
  border-color: red;
}
</style>