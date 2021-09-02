<template>
  <div class="dropdown dropdown-filter" @click="toggleDropdown">
    <div class="dropdown-text">{{ selectedText }}</div>
    <div class="dropdown-icon" :class="{ rotate: isRotate }">
      <div class="btn-dropdown"></div>
    </div>
      <div class="box-icon"></div>
    <div
      class="dropdown-data"
      :class="{ 'dropdown-hidden': isClose }"
      @click="getCurrentItem"
    >
      <div
        class="dropdown-item"
        v-for="(item, index) in items"
        :key="item.Value"
        @click="selectItem(index)"
        :value="item.Value"
        :class="{ 'dropdown-selected': activeItem(index) }"
      >
        <div class="dropdown-item-text">
          {{ item.Text }}
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import axios from "axios";
export default {
  name: "BaseDropdown",
  props: ["data", "url"],
  async created() {
    await this.getItem();
    this.selectItem(0);
  },
  data() {
    return {
      isClose: true,
      selectedText: "",
      selectedValue: "",
      items: [],
      currentIndex: 0,
      isRotate: false,
    };
  },

  methods: {
    /**--------------------------------------------------------------------
     * Hàm bắt sự kiện click gửi value sang cho bên employeeDetail
     * CreatedBy: LNT(28/08/2021)
     */
    getCurrentItem() {
      this.$emit("get", this.selectedValue);
    },

    /**----------------------------------------------
     * Hàm xử lý việc toggle dropdown
     * CreatedBy: LNT(28/08/2021)
     */
    toggleDropdown() {
      this.isClose = !this.isClose;
      this.isRotate = !this.isRotate;
    },

    /**---------------------------------------------------
     * Hàm call API để lấy dữ liệu
     * CreatedBy: LNT(28/08/2021)
     */
    async getItem() {
      try {
        // push dữ liệu trong data vào trong items
        this.data.forEach((element) => {
          this.items.push(element);
        });
        // check url có null
        if (!this.checkUrl(this.url)) {
          let response = await axios.get(this.url);
          response.data.forEach((element) => {
            this.items.push({
              Text: element[this.fields[1]],
              Value: element[this.fields[0]],
            });
          });
        }
      } catch (error) {
        console.log(error);
      }
    },

    /**--------------------------------------------
     * Hàm select item
     * CreatedBy: LNT(28/08/2021)
     */
    selectItem(index) {
      this.selectedText = this.items[index].Text;
      this.selectedValue = this.items[index].Value;
      this.currentIndex = index;
    },

    /**-----------------------------------------------------
     * Hàm check index của item
     * CreatedBy: LNT(28/08/2021)
     */
    activeItem(index) {
      return this.currentIndex == index ? true : false;
    },

    /**------------------------------------------------------------------
     * Hàm check url truyền vào
     * CreatedBy: LNT(28/08/2021)
     */
    checkUrl(url) {
      return typeof url === undefined || url === null || url === ""
        ? true
        : false;
    },

    /**--------------------------------------------------------------
     * Hàm trả về index của item khi so sánh value
     * CreatedBy: LNT(28/08/2021)
     */
    getIndexByValue() {
      return this.items.findIndex((element, index) => {
        if (element.Value === this.value) {
          return index;
        }
      });
    },

    /**------------------------------------------------------
     * Hàm bắt sự kiện nút reload
     * khi reload sẽ đưa text dropdown về mặc định
     * CreatedBy: LNT(28/08)
     */
    setTextDefault() {
      this.selectedText = this.items[0].Text;
      this.selectedValue = this.items[0].Value;
      this.currentIndex = 0;
      this.$emit("setValueDefaultDropdown",this.selectedValue);
    },

    /**----------------------------------------------------------------------
     * Hàm check event khi click ra bên ngoài dropdown
     * CreatedBy: LNT(28/08)
     */
    close(e) {
      if (!this.$el.contains(e.target)) {
        this.isClose = true;
        this.isRotate = false;
      }
    },
  },

  mounted() {
    document.addEventListener("click", this.close);
  },
};
</script>

<style scoped>
@import url(../../css/base/dropdown.css);
</style>