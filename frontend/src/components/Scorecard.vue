<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <v-card>
          <v-card-title>
            <v-text-field
              label="Scorecard Name"
              v-model="scorecard.title"
              :disabled="!isNew || !isModify"
            ></v-text-field>
          </v-card-title>
          <div v-if="isNew || isModify">
            <v-card-text>
              <v-form class="px-">
                <v-btn class="success mx-0 mt-3" @click.prevent="saveScorecard">Save Scorecard</v-btn>
                <v-btn class="primary mx-0 mt-3" @click="addScorecardMatchObject">Add a match</v-btn>
              </v-form>
            </v-card-text>

            <div v-if="scorecard.scorecardMatches && scorecard.scorecardMatches.length > 0">
              <ScorecardMatch
                v-bind:key="match.id"
                v-for="match in scorecard.scorecardMatches"
                v-bind:scorecardMatch="match"
                v-bind:isNew="isNew"
                v-bind:isModify="isModify"
              />
            </div>
          </div>
          <div v-if="modify">
            <v-card-text>
              <v-form class="px-">
                <v-btn class="primary mx-0 mt-3" @click="modifyScorecard">Modify</v-btn>
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
  props: ["scorecard", "isNew", "isModify", "modify"],
  methods: {
    addScorecardMatchArray() {
      this.$set(this.scorecard, "scorecardMatches", []);
    },
    addScorecardMatchObject() {
      const match = {
        title: ""
      };
      var count = this.scorecard.scorecardMatches.length;
      this.$set(this.scorecard.scorecardMatches, count, match);
    },
    saveScorecard() {
      this.$emit("save-scorecard", this.scorecard);
    },
    modifyScorecard() {
      this.$emit("modify-scorecard", this.scorecard);
    }
  },
  created() {
    if (this.isNew) this.addScorecardMatchArray();
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
