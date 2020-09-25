<template>
  <div>
    <v-navigation-drawer v-model="drawer" app right>
      <v-list>
        <v-list-item v-for="link in links" :key="link.text" router :to="link.route">
          <v-list-item-action>
            <v-icon>{{ link.icon }}</v-icon>
          </v-list-item-action>
          <v-list-item-content>
            <v-list-item-title>{{ link.text }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar app color="blue" dark shrink-on-scroll max-height="8%">
      <v-spacer>
        <v-toolbar-title>
          <span class="font-weight-light">Couch</span>
          <span>Score</span>
        </v-toolbar-title>
      </v-spacer>
      <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
    </v-app-bar>
  </div>
</template>

<script>
export default {
  name: "NavBar",
  data() {
    return {
      drawer: false,
      links: [
        { icon: "home", text: "Home", route: "/" },
        { icon: "person", text: "Me", route: "/about" },
        { icon: "bookmarks", text: "My Scorecards", route: "/my-scorecards" },
        { icon: "add", text: "Create Scorecard", route: "/create" },
        { icon: "list", text: "Todo List", route: "/todo-home" },
        { icon: "mdi-logout", text: "Logout", route: "/logout" }
      ]
    };
  }
};
</script>

<style scoped></style>
