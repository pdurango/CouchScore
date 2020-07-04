import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import vuetify from "./plugins/vuetify";
import axios from "axios";
import "material-design-icons-iconfont/dist/material-design-icons.css"; //https://github.com/google/material-design-icons

Vue.config.productionTip = false;
axios.defaults.baseURL = "https://localhost:5001/api";

new Vue({
   router,
   vuetify,
   render: (h) => h(App),
}).$mount("#app");
