<template>
  <v-app>
    <v-main class="grey lighten-4">
      <NavBar />
      <Header />
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
import Header from "./components/layout/Header.vue";
import NavBar from "./components/NavBar.vue";

export default {
  name: "App",

  components: {
    Header,
    NavBar
  },

  data: () => ({
    //
  })
};
</script>
