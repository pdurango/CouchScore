<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <div>
          <h1>Created By Me</h1>
          <div v-if="createdScorecards.length > 0">
            <div v-bind:key="scorecard.id" v-for="scorecard in createdScorecards">
              <Scorecard v-bind:scorecard="scorecard" v-bind:isEditable="true" />
            </div>
          </div>
          <div v-else>You have not created any scorecards.</div>
        </div>
        <div>
          <h1>Participating</h1>
          <div v-if="linkedScorecards.length > 0">
            <div v-bind:key="scorecard.id" v-for="scorecard in linkedScorecards">
              <Scorecard v-bind:scorecard="scorecard" v-bind:isEditable="false" />
            </div>
          </div>
          <div v-else>You have no scorecards.</div>
        </div>
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
      createdScorecards: [], //Only contains scorecards created by user
      linkedScorecards: [] //Contains scorecards created by user AND all other linked ones
      //isEditable: Boolean
    };
  },
  methods: {
    loadScorecards() {
      console.log("loadScorecards");
      this.$api
        .get("/scorecards/linked")
        .then(
          res => (
            (this.linkedScorecards = res.data),
            console.log("deez"),
            this.separateScorecards()
          )
        )
        .catch(err => console.log(err));
    },
    separateScorecards() {
      console.log("nuts");
      const userId = localStorage.getItem("userId");
      console.log("userId: " + userId);
      for (var i = 0; i < this.linkedScorecards.length; i++) {
        console.log(this.linkedScorecards[i]);
        if (this.linkedScorecards[i].createdBy == userId) {
          this.createdScorecards.push(this.linkedScorecards[i]);
        }
      }
    }
  },
  created() {
    this.loadScorecards();
    //this.isEditable = false;
  }
};
</script>

<style scoped></style>
