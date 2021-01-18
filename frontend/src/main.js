import Vue from "vue";
import App from "./App.vue";
import router from "./router";
import vuetify from "./plugins/vuetify";
import "material-design-icons-iconfont/dist/material-design-icons.css"; //https://github.com/google/material-design-icons
import api from "./services/api";

Vue.config.productionTip = false;
Vue.prototype.$api = api;
api.defaults.timeout = 10000;
api.interceptors.request.use(
   (config) => {
      console.log("api.interceptors.request");
      //console.log("Request:", JSON.stringify(config, null, 2));
      const token = localStorage.getItem("authToken");
      console.log(token);

      if (token) {
         config.headers.common["Authorization"] = `Bearer ${token}`;
      }
      return config;
   },
   (error) => {
      return Promise.reject(error);
   }
);
api.interceptors.response.use(
   (response) => {
      if (response.status === 200 || response.status === 201) {
         //console.log(response);
         return Promise.resolve(response);
      } else {
         return Promise.reject(response);
      }
   },
   (error) => {
      switch (error.status) {
         case 400:
            //do something
            break;

         case 401:
            alert("session expired");
            break;
         case 403:
            router.replace({
               path: "/login",
               query: { redirect: router.currentRoute.fullPath },
            });
            break;
         case 404:
            alert("page not exist");
            break;
         case 502:
            setTimeout(() => {
               router.replace({
                  path: "/login",
                  query: {
                     redirect: router.currentRoute.fullPath,
                  },
               });
            }, 1000);
      }
      return Promise.reject(error.response);
   }
);

new Vue({
   router,
   vuetify,
   render: (h) => h(App),
}).$mount("#app");

/*
axios.interceptors.request.use(
   (config) => {
      var token = localStorage.getItem("authToken");

      console.log(this.router);
      if (this.$router.path != "/login") {
         if (token) config.headers["Authorization"] = `Bearer ${token}`;
         else this.$router.push("/login");
      }
      return config;
   },

   (error) => {
      return Promise.reject(error);
   }
);

axios.interceptors.response.use((response) => {
   console.log("Response:", JSON.stringify(response, null, 2));

   (error) => {
      var statusCode = error.response.status;
      console.log(statusCode);
      console.log(error.response.data);
      console.log(error.response.headers);

      if (statusCode == 401) {
         localStorage.removeItem("authToken");
         this.$router.push("/login");
      }
      return Promise.reject(error);
   };
});
*/
