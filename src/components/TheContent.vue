<template>
  <div class="content-container">
    <EmployeeList/>
  </div>
</template>

<script>
import EmployeeList from './../views/EmployeeList.vue';
export default {
  components: {
    EmployeeList,
  }
}
</script>

<style scoped>
@import "../css/layout/content.css";


</style>