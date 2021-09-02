<template>
  <div :class="[nameDropdown]">
    <label
      :id="id"
      :class="['c-dropdown', dropdownType]"
      :myurl="myUrl"
      :itemid="itemId"
      :itemname="itemName"
      style="margin-left: 0; padding-left: 0:"
    >
      <div
        class="c-dropdown-header-wrapper"
        @click="toggleItems"
        :value="currentId"
        :tabindex="tabindex"
      >
        <span class="c-dropdown-value" :id="dropdownValue" style="margin-left: 0;">
          {{ currentName }}
        </span>
        <i
          :class="['fas fa-chevron-down icon-down', opened ? 'move-up' : '']"
        ></i>
      </div>
      <ul :class="['c-dropdown-list', opened ? 'isshow' : 'isnone']">
        <li
          :class="['c-dropdown-item', item[itemId] == currentId ? 'active' : '']"
          v-for="item in itemlist"
          :key="item[itemId]"
          @click="clickItem(item[itemId], item[itemName])"
        >
          {{ item[itemName] }}
        </li>
      </ul>
    </label>
  </div>
</template>

<script>
import axios from "axios";
import eventBus from '../../eventBus';
export default {
  name: "BaseDropdown",
  data() {
    return {
      itemlist: [],
      currentId: "-1",
      currentName: "",
      opened: false,
      defaultId: "-1",
    };
  },
  props: {
    id: String,
    nameDropdown: String,
    dropdownType: String,
    propName: String,
    dropdownValue: String,
    myUrl: String,
    itemId: String,
    itemName: String,
    defaultName: String,
    selectedId: String,
    tabindex: String,
    
  },
  created() {
    this.loadDropdownData();
    this.currentName = this.defaultName;
    this.initChoice();
    eventBus.$on("reloadDropdown", () => {
      this.currentName = this.defaultName;
    });
    eventBus.$on("refreshDropdown", () => {
      this.currentName = this.defaultName;
    });
  },
  watch: {
    selectedId: function () {
      this.initChoice();
    },
  },
  methods: {
    loadDropdownData() {
      switch (this.itemName) {
        case "Action":
          this.itemlist = [
            {
              Gender: 1,
              GenderName: "Nhân bản",
            },
            {
              Gender: 2,
              GenderName: "Xóa",
            },
            {
              Gender: 3,
              GenderName: "Ngừng sử dụng",
            },
          ];
          break;
        case "WorkStatusName":
          this.itemlist = [
            {
              WorkStatus: 0,
              WorkStatusName: "Đang thử việc",
            },
            {
              WorkStatus: 1,
              WorkStatusName: "Đang làm việc",
            },
            {
              WorkStatus: 2,
              WorkStatusName: "Đang nghỉ phép",
            },
            {
              WorkStatus: 3,
              WorkStatusName: "Đã nghỉ việc",
            },
            
          ];
          break;
        case "StoreName":
          this.itemlist = [
            {
              StoreId: 0,
              StoreName: "Nhà hàng Biển Đông",
            },
            {
              StoreId: 1,
              StoreName: "Nhà hàng Phú Quốc",
            },
            {
              StoreId: 2,
              StoreName: "Nhà hàng Đà Nẵng",
            },
            {
              StoreId: 3,
              StoreName: "Nhà hàng Hà Nội",
            },
          ];
          break;
        default:
          if (this.myUrl) {
            axios
              .get(`https://localhost:44338/api/${this.myUrl}`)
              .then((res) => {
                this.itemlist = res.data;
              })
              .catch((error) => {
                console.log(error);
              });
          }
          break;
      }
    },
    initChoice() {
      let vm = this;
      if ((vm.selectedId + "").length > 0) {
        vm.itemlist.forEach((item) => {
          if (vm.selectedId == item[vm.itemId]) {
            vm.currentName = item[vm.itemName];
            vm.currentId = item[vm.itemId];
            vm.selectedItem = vm.currentId;
            
          }
        });
      } else {
        vm.currentId = -1;
        vm.currentName = "";
      }
    },
    clickItem(itemValue, itemName) {
      this.currentId = itemValue;
      this.currentName = itemName;
      this.opened = false;
      this.$emit("input", itemValue);
      // this.$emit("get",vm.currentName);
    },
    toggleItems(){
      this.opened = !this.opened;
    },
    close(e) {
      if (!this.$el.contains(e.target)) {
        this.opened = false;
        // this.isRotate = false;
      }
    },
    /**--------------------------------------------------------------------
     * Hàm bắt sự kiện click gửi value sang cho bên employeeDetail
     * CreateBy: LNT(2/8/2021)
     */
    getCurrentItem() {
      this.$emit("get", this.currentName);
    },
  },
  mounted() {
    document.addEventListener("click", this.close);
  },
};
</script>
<style scoped>
@import "../../css/employee/dropdown.css";
/* @import "../../css/base/icon.css"; */

.c-dropdown-list.isshow {
  display: block;
}
.c-dropdown-list.isnone {
  display: none;
}
#store{
  width: 200px;
  font-size: 15px;
  border: none;
}


</style>

