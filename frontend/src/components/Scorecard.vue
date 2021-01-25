<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <v-card>
          <v-card-title>
            <v-text-field
              label="Scorecard Name"
              v-model="_scorecard.title"
              :disabled="!isNew && !isModify"
            ></v-text-field>
          </v-card-title>
          <div v-if="isNew || isModify">
            <v-card-text>
              <v-form class="px-">
                <v-btn class="success mx-0 mt-3" @click.prevent="saveScorecard">Save Scorecard</v-btn>
                <v-btn class="primary mx-0 mt-3" @click="addScorecardMatchObject">Add a match</v-btn>
                <v-btn class="mx-0 mt-3" @click="deleteScorecard">Delete</v-btn>
              </v-form>
            </v-card-text>
            <div v-if="_scorecard.scorecardMatches && _scorecard.scorecardMatches.length > 0">
              <ScorecardMatch
                v-on:update-scorecard-match="updateScorecardMatch"
                v-for="match in _scorecard.scorecardMatches"
                v-bind:key="match.id"
                v-bind:scorecardMatch="match"
                v-bind:isNew="isNew"
                v-bind:isModify="isModify"
              />
            </div>
          </div>
          <div v-if="modifyButton">
            <v-card-text>
              <v-form class="px-">
                <v-btn class="primary mx-0 mt-3" @click="modifyScorecard">Modify</v-btn>
                <v-btn class="mx-0 mt-3" @click="deleteScorecard">Delete</v-btn>
              </v-form>
            </v-card-text>
          </div>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import ScorecardMatch from "./ScorecardMatch.vue";
export default {
  name: "Scorecard",
  components: {
    ScorecardMatch
  },
  props: ["scorecard", "isNew", "isModify", "modifyButton"],
  computed: {
    _scorecard: {
      get: function() {
        console.log("getter called with scorecard");
        return this.scorecard;
      },
      set: function(scorecard) {
        console.log("setter called with scorecard");
        this.$emit("update-scorecard", scorecard);
      }
    }
  },
  methods: {
    addScorecardMatchArray() {
      this.$set(this._scorecard, "scorecardMatches", []);
    },
    addScorecardMatchObject() {
      const match = {
        title: ""
      };
      var count = this._scorecard.scorecardMatches.length;
      this.$set(this._scorecard.scorecardMatches, count, match);
    },
    deleteScorecard() {
      this.$api
        .delete("/scorecards/" + this._scorecard.id)
        .then((this._scorecard = {}))
        .catch(err => console.log(err));
      this.$emit("save-scorecard", this._scorecard);
    },
    saveScorecard() {
      this.$emit("save-scorecard", this._scorecard);
    },
    modifyScorecard() {
      this.$emit("modify-scorecard", this._scorecard);
    },
    updateScorecardMatch(scorecardMatch, key) {
      this.$set(this._scorecard.scorecardMatches, key, scorecardMatch);
    }
  },
  created() {
    if (this.isNew || (this.modify && this._scorecard.ScorecardMatches == null))
      this.addScorecardMatchArray();
  }
};
</script>

<style scoped>
form {
  display: flex;
}

input[type="text"] {
  flex: 10;
  padding: 5px;
}

input[type="submit"] {
  flex: 2;
}

.centered {
  border: 2px solid green;
  padding: 10px;
  border-radius: 25px;
  position: fixed;
  height: 80%;
  width: 50%;
  top: 50%;
  left: 50%;
  /* bring your own prefixes */
  transform: translate(-50%, -50%);
}
</style>
