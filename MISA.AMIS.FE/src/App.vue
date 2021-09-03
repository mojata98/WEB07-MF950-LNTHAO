<template>
  <div id="app">
    <TheHeader/>
    <TheMenu/>
    <TheContent />
    <BasePopup/>
  </div>
</template>

<script>
import TheHeader from './components/TheHeader.vue';
import TheMenu from './components/TheMenu.vue';
import TheContent from './components/TheContent.vue';
import BasePopup from './components/base/BasePopup.vue';
export default {
  name: 'App',
  components: {
    TheHeader,
    TheMenu, 
    TheContent,
    BasePopup,
  }
}
</script>

<style>
@import './css/main.css';

</style>
