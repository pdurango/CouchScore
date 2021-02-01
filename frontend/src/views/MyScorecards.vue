<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <div v-if="Object.keys(this.currentScorecard).length > 0">
          <Scorecard
            v-on:save-scorecard="saveScorecard"
            v-bind:scorecard="currentScorecard"
            v-bind:isModify="true"
          />
        </div>
        <div v-else>
          <div>
            <h1>Created By Me</h1>
            <div v-if="_createdScorecards.length > 0">
              <Scorecard
                v-on:load-scorecards="loadScorecards"
                v-on:modify-scorecard="modifyScorecard"
                v-for="scorecard in _createdScorecards"
                v-bind:key="scorecard.id"
                v-bind:scorecard="scorecard"
                v-bind:modifyButton="true"
              />
            </div>
            <div v-else>You have not created any scorecards.</div>
          </div>
          <div>
            <h1>Participating</h1>
            <div v-if="scorecards.length > 0">
              <Scorecard
                v-for="scorecard in scorecards"
                v-bind:key="scorecard.id"
                v-bind:scorecard="scorecard"
              />
            </div>
            <div v-else>You have no scorecards.</div>
          </div>
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
      scorecards: [], //Contains scorecards created by user AND all other linked ones
      currentScorecard: {} //The scorecard currently being modified
    };
  },
  computed: {
    _createdScorecards: function() {
      //Only contains scorecards created by user
      const userId = localStorage.getItem("userId");
      return this.scorecards.filter(i => i.createdBy == userId);
    }
  },
  methods: {
    loadScorecards() {
      console.log("loadScorecards");
      this.$api
        .get("/scorecards")
        .then(
          res => {
            this.scorecards = res.data;
            this.currentScorecard = {};
          }
        )
        .catch(err => console.log(err));
    },

    /*
     * Called from children
     */
    modifyScorecard(scorecard) {
      this.$api
        .get("/scorecards/" + scorecard.id)
        .then(res => (this.currentScorecard = res.data))
        .catch(err => console.log(err));
    },
    saveScorecard() {
      this.loadScorecards();
    },
  },
  created() {
    this.loadScorecards();
    //this.isEditable = false;
  }
};
</script>

<style scoped></style>
