import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import vuetify from "./plugins/vuetify";
import axios from "axios";

Vue.config.productionTip = false;
axios.defaults.baseURL = "https://localhost:5001";
//axios.defaults.headers.common["Access-Control-Allow-Origin"] = "*";

new Vue({
   router,
   vuetify,
   render: (h) => h(App),
}).$mount("#app");
