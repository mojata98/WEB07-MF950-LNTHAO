import Vue from 'vue'
import App from './App.vue'
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

Vue.prototype.moment = moment
Vue.component('ValidationProvider', ValidationProvider);
Vue.component('ValidationObserver', ValidationObserver);
Vue.component('datepicker', DatePicker);
Vue.component('paginate', Paginate)
Vue.use(Toast, {
    transition: "Vue-Toastification__bounce",
    maxToasts: 10,
    newestOnTop: true
});
Object.keys(rules).forEach(rule => {
    extend(rule, {
        ...rules[rule],
        message: messages[rule]
    });
});
new Vue({
    render: h => h(App),
}).$mount('#app')