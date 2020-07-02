<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <Scorecard
          v-bind:scorecard="scorecard"
          v-bind:isNewScorecard="isNewScorecard"
          v-on:save-scorecard="saveScorecard"
        />
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Scorecard from "../components/Scorecard.vue";
import axios from "axios";

export default {
  name: "Create",
  components: {
    Scorecard
  },
  data() {
    return {
      scorecard: { createdBy: 1 }, //fix this - dont add 1 manually - bad
      isNewScorecard: Boolean
    };
  },
  methods: {
    saveScorecard(newScorecard) {
      var id = newScorecard;
      axios
        .post("/scorecards", newScorecard)
        .then(res => (id = res.id))
        .catch(err => console.log(err));

      console.log(id);
    }
  },
  created() {
    this.isNewScorecard = true;
  }
};
</script>

<style scoped>
</style>
