<template>
  <v-app>
    <v-main class="grey lighten-4">
      <NavBar v-if="this.$route.name !== 'Login'" />
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
import NavBar from "./components/NavBar.vue";

export default {
  name: "App",

  components: {
    NavBar
  },

  data: () => ({
    //
  })
};
</script>
