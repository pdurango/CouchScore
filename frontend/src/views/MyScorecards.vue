<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <div v-if="scorecards.length > 0">
          <div v-bind:key="scorecard.id" v-for="scorecard in scorecards">
            <Scorecard v-bind:scorecard="scorecard" v-bind:isEditable="isEditable" />
          </div>
        </div>
        <div v-else>You have no scorecards.</div>
        <!--
        <div v-bind:key="scorecard.id" v-for="scorecard in scorecards">
          <Scorecard v-bind:scorecard="scorecard" v-bind:isEditable="this.isEditable" />
        </div> 
        -->
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Scorecard from "../components/Scorecard.vue";

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
      console.log("loadScorecards");
      this.$api
        .get("/scorecards")
        .then(
          res => (
            (this.scorecards = res.data), console.log(this.scorecards.length)
          )
        )
        .catch(err => console.log(err));
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