<template>
  <v-app>
    <v-main>
      <Header/>
      <router-view/>
    </v-main>
  </v-app>
</template>

<script>
import Header from './components/layout/Header.vue'

export default {
  name: 'App',

  components: {
    Header,
  },

  data: () => ({
    //
  }),
};
</script>
