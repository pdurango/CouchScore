<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <v-card color="#bdb9b9">
          <v-card-title>
            <v-text-field label="Scorecard Match Name" v-model="_scorecardMatch.title"></v-text-field>
          </v-card-title>
          <v-card-text>
            <v-btn class="primary mx-0 mt-3" @click="addScorecardMatchOptionObject">Add an option</v-btn>
            <v-btn class="mx-0 mt-3" @click="removeScorecardMatchObject">Delete</v-btn>

            <div v-if="isNew || isModify">
              <ScorecardMatchOption
                v-on:update-scorecard-match-option="updateScorecardMatchOption"
                v-for="matchOptions in _scorecardMatch.scorecardMatchOptions"
                v-bind:key="matchOptions.id"
                v-bind:scorecardMatchOption="matchOptions"
                v-bind:isNew="isNew"
                v-bind:isModify="isModify"
              />
            </div>
            <v-radio-group v-else v-model="_scorecardMatch.userSelection" :mandatory="false">
              <ScorecardMatchOption
                v-bind:key="matchOptions.id"
                v-for="matchOptions in _scorecardMatch.scorecardMatchOptions"
                v-bind:scorecardMatchOption="matchOptions"
              />
            </v-radio-group>
          </v-card-text>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import ScorecardMatchOption from "./ScorecardMatchOption.vue";
export default {
  name: "ScorecardMatch",
  components: {
    ScorecardMatchOption
  },
  props: ["scorecardMatch", "isNew", "isModify"],
  computed: {
    _scorecardMatch: {
      get: function() {
        console.log("getter called with scorecardMatch");
        return this.scorecardMatch;
      },
      set: function(scorecardMatch) {
        console.log("setter called with scorecardMatch");
        this.$emit("update-scorecard-match", scorecardMatch, this.$vnode.key);
      }
    }
  },
  methods: {
    addScorecardMatchOptionArray() {
      this.$set(this._scorecardMatch, "scorecardMatchOptions", []);
    },
    addScorecardMatchOptionObject() {
      const option = {
        title: "",
        userSelection: ""
      };
      var count = this._scorecardMatch.scorecardMatchOptions.length;
      this.$set(this._scorecardMatch.scorecardMatchOptions, count, option);
    },
    removeScorecardMatchObject() {
      this.$delete(this._scorecardMatch);
    },
    updateScorecardMatchOption(scorecardMatchOption, key) {
      this.$set(
        this._scorecardMatch.scorecardMatchOptions,
        key,
        scorecardMatchOption
      );
    }
  },
  created() {
    console.log("ScorecardMatch sNew: " + this.isNew);
    console.log("ScorecardMatch isModify: " + this.isModify);
    if (
      this.isNew ||
      (this.modify &&
        !Object.prototype.hasOwnProperty.call(
          this._scorecardMatch,
          "ScorecardMatchOptions"
        ))
    )
      this.addScorecardMatchOptionArray();
  }
};
</script>

<style scoped>
.centered2 {
  border: 2px solid red;
  padding: 25px 20px 20px;
  border-radius: 25px;
  height: 80%;
  width: 80%;
}
</style>
