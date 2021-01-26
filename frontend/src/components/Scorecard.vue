<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <v-card>
          <v-card-title>
            <v-text-field
              label="Scorecard Name"
              v-model="currentScorecard.title"
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
            <div
              v-if="currentScorecard.scorecardMatches && currentScorecard.scorecardMatches.length > 0"
            >
              <ScorecardMatch
                v-on:update-scorecard-match="updateScorecardMatch"
                v-for="match in currentScorecard.scorecardMatches"
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
  data() {
    return {
      currentScorecard: {}
    };
  },
  methods: {
    addScorecardMatchArray() {
      this.$set(this.currentScorecard, "scorecardMatches", []);
    },
    addScorecardMatchObject() {
      const match = {
        title: ""
      };
      const count = this.currentScorecard.scorecardMatches.length;
      this.$set(this.currentScorecard.scorecardMatches, count, match);
    },
    deleteScorecard() {
      this.$api
        .delete("/scorecards/" + this.currentScorecard.id)
        .then((this.currentScorecard = {}), this.$emit("load-scorecards"))
        .catch(err => console.log(err));
    },
    saveScorecard() {
      if (this.isNew) {
        this.$api
          .post("/scorecards", this.currentScorecard)
          .then(
            res => (this.currentScorecard.id = res.data.id),
            this.$emit("save-scorecard", this.currentScorecard)
          )
          .catch(err => console.log(err));
      } else if (this.isModify) {
        this.$api
          .put("/scorecards/" + this.currentScorecard.id, this.currentScorecard)
          .then(
            res => (this.currentScorecard = res.data),
            this.$emit("save-scorecard", this.currentScorecard)
          )
          .catch(err => console.log(err));
      }
    },
    modifyScorecard() {
      this.$emit("modify-scorecard", this.currentScorecard);
    },
    updateScorecardMatch(scorecardMatch, key) {
      this.$set(this.currentScorecard.scorecardMatches, key, scorecardMatch);
    }
  },
  created() {
    this.currentScorecard = this.scorecard;
    if (
      this.isNew ||
      (this.isModify &&
        !Object.prototype.hasOwnProperty.call(
          this.currentScorecard,
          "scorecardMatches"
        ))
    ) {
      this.addScorecardMatchArray();
    }
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
