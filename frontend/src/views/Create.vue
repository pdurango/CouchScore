<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <Scorecard
          v-bind:scorecard="scorecard"
          v-bind:isNew="isEditable"
          v-on:save-scorecard="saveScorecard"
        />
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Scorecard from "../components/Scorecard.vue";

export default {
  name: "Create",
  components: {
    Scorecard
  },
  data() {
    return {
      scorecard: {},
      isEditable: Boolean,
      id: Number
    };
  },
  methods: {
    saveScorecard(scorecard) {
      this.scorecard = scorecard;
      this.$router.push("/my-scorecards");
    }
  },
  created() {
    this.isEditable = true;
  }
};
</script>

<style scoped></style>
