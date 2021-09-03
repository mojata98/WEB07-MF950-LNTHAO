<template>
    <div 
      class="action-bottom"  
      :class="{'isShow': isShow}"
      :style="{'top': valueTop}"
      >
      <div class="item" style="cursor: pointer;" @click="cloneEmployee">Nhân bản</div>
      <div class="item" style="cursor: pointer;" @click="deleteEmployee">Xóa</div>
      <div class="item disable">Ngừng sử dụng</div>
    </div>
</template>

<script>
import eventBus from '../../eventBus';
export default {
  data() {
    return {
      isShow: true, // ẩn hiện menu
      id: "", // ID nhân viên cần thực hiện chức năng       
      valueTop: "62px", // Vị trí mặc định cho hàng đầu tiên
      employee: {}, // Thông tin nhân viên thực hiện chức năng
    }
  },

  created() {
    /**--------------------------------------------------
     * Đặt vị trí cho action-container theo index
     * CreatedBy: LNT (01/09)
     */
    eventBus.$on("passDataToContextMenu", (object) => {
      this.employee = object.employeeObject;
      this.id = object.employeeObject.EmployeeId;
      this.isShow = !this.isShow;
      if (object.index < object.pageSize - 1) { // 1 -> n-1
        this.valueTop = String((object.index * 47)+ 15 + "px");
      }
      else if (object.index >= object.pageSize - 1) { // n-1 && n
        this.valueTop = String((object.index * 47)+ 15 - 130 + "px");
      }
    });
  },

  methods: {
    /**-----------------------------------------------
     * Tạo sự kiện nhân bản
     * CreatedBy: LNT (02/09)
     */
    cloneEmployee(){
      this.isShow = true;
      eventBus.$emit("cloneEmployee", this.employee);
      eventBus.$emit("openForm");
    },

    /**-------------------------------------------
     * Tạo sự kiện xóa nhân viên
     * CreatedBy: LNT (02/09)
     */
    deleteEmployee(){
      this.isShow = true;
      eventBus.$emit("deleteMode", this.employee);
    }
  },
}
</script>

<style scoped>
.isShow{
  display: none;
}

.action-bottom{
  height: 101px;
  width: 143px;
  margin-top: 8px;
  border: 1px solid #c7c7c7;
  z-index: 10;
  background-color: #fff;
  position: absolute;
  right: 10px;
}

.item{
  height: 32px;
  width: 138px;
  align-items: center;
  display: flex;
  padding-left: 5px;
}

.item:hover{
  background-color: #eceef1;
  color: #2ca01c;
}

.disable{
  color: #c7c7c7;
  opacity: 0.5;
}

.disable:hover{
  color: #c7c7c7;
  opacity: 0.5;
  background-color: #fff;
}
</style>