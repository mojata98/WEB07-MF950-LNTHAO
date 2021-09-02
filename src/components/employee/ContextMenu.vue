<template>
  <!-- <div class="action-container " 
      >
    <div class="action-top" >
      <div class="icon-container">
        <div class="icon"></div>
      </div>
    </div> -->

    <div 
      class="action-bottom"  
      :class="{'isShow': isShow}"
      :style="{'top': valueTop}"
      >
      <div class="item" style="cursor: pointer;" @click="cloneEmployee">Nhân bản</div>
      <div class="item" style="cursor: pointer;" @click="deleteEmployee">Xóa</div>
      <div class="item disable">Ngừng sử dụng</div>
    </div>
  <!-- </div> -->

</template>

<script>
import eventBus from '../../eventBus';
export default {
  props: {
    isHiddenOption: {
      type: Boolean,
      default: true,
    },
    
  },

  data() {
    return {
      isShow: true, // ẩn hiện menu
      id: "", // ID nhân viên cần thực hiện chức năng       
      valueTop: "62px", // Vị trí mặc định cho hàng đầu tiên
      employee: {}, // Thông tin nhân viên thực hiện chức năng
    }
  },

  created() {
    /**
     * Đặt vị trí cho action-container theo index
     * CreatedBy: LNT (01/09)
     */
    eventBus.$on("passDataToContextMenu", (object) => {
      this.employee = object.employeeObject;
      this.id = object.employeeObject.EmployeeId;
      this.isShow = !this.isShow;
      if (object.index < object.pageSize - 1) {
        this.valueTop = String((object.index * 47)+ 15 + "px");
      }
      else if (object.index >= object.pageSize - 1) {
        this.valueTop = String((object.index * 47)+ 15 - 130 + "px");
      }
    });
  },

  methods: {
    cloneEmployee(){
      console.log(this.id);
    },

    deleteEmployee(){
      eventBus.$emit("deleteMode", this.employee);
    }
  },
}
</script>

<style scoped>
/* @import url(../../css/employee/list.css); */

.isShow{
  display: none;
}

.action-container{
  position: absolute;
  height: 126px;
  width: 145px;
  background-color: transparent;
  display: block;
  z-index: 10;
}

.action-top{
  height: 16px;
  width: 145px;
  background-color: transparent;
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

.icon-container{
  position: absolute;
  height: 16px;
  width: 26px;
  right: 10px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  border: 1px solid blue;
}

.icon{
  height: 16px;
  width: 16px;
  background: url('../../assets/img/Sprites.64af8f61.svg') no-repeat -896px -359px;
}

/* .icon:focus{
  border-color: blue;
}

.icon-container:hover{
  border-color: blue;
} */

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