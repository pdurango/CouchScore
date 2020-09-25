<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <div v-if="scorecards.length > 0">
          <Scorecard
            v-bind:key="scorecard.id"
            v-for="scorecard in scorecards.scorecard"
            v-bind:isEditable="this.isEditable"
          />
        </div>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Scorecard from "../components/Scorecard.vue";
import axios from "axios";

export default {
  name: "MyScorecards",
  components: {
    Scorecard
  },
  data() {
    return {
      scorecards: [],
      isEditable: Boolean
    };
  },
  methods: {
    loadScorecards() {
      axios
        .get("/scorecards")
        .then(res => (this.scorecards = res.scorecards))
        .catch(err => console.log(err));

      console.log("suh");
    }
  },
  created() {
    this.loadScorecards();
    this.isEditable = false;
  }
};
</script>

<style scoped>
</style>