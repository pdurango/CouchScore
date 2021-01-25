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
      //console.log("api.interceptors.request");
      //console.log("Request:", JSON.stringify(config, null, 2));
      const token = localStorage.getItem("authToken");
      //console.log(token);

      if (token) config.headers.common["Authorization"] = `Bearer ${token}`;
      else {
         router.replace({
            path: "/login",
            query: {
               redirect: router.currentRoute.fullPath,
            },
         });
      }
      return config;
   },
   (error) => {
      return Promise.reject(error);
   }
);

//todo  - Should eventually clear localstorage on certain 40X errors
//localStorage.clear();
api.interceptors.response.use(
   (response) => {
      if (response.status === 200 || response.status === 201 || response.status === 204) {
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
               query: {
                  redirect: router.currentRoute.fullPath,
               },
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
