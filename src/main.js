import Vue from 'vue'
import App from './App.vue'
// import axios from 'axios'
// import VueAxios from 'vue-axios'
import moment from 'moment';
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";
import { ValidationProvider, ValidationObserver, extend } from 'vee-validate';
import * as rules from 'vee-validate/dist/rules';
import { messages } from 'vee-validate/dist/locale/vi.json';
import DatePicker from 'vue2-datepicker';
import 'vue2-datepicker/index.css';
import Paginate from 'vuejs-paginate'
Vue.config.productionTip = false

// Vue.use(VueAxios, axios)
Vue.prototype.moment = moment
Vue.use(Toast, {
    transition: "Vue-Toastification__bounce",
    maxToasts: 10,
    newestOnTop: true
});
Object.keys(rules).forEach(rule => {
    extend(rule, {
        ...rules[rule], // copies rule configuration
        message: messages[rule] // assign message
    });
});
// Register it globally
Vue.component('ValidationProvider', ValidationProvider);
Vue.component('ValidationObserver', ValidationObserver);
Vue.component('datepicker', DatePicker);

Vue.component('paginate', Paginate)
new Vue({
    render: h => h(App),
}).$mount('#app')