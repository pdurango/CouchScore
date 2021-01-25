<template>
  <v-container v-if="isNew || isModify" grid-list-xl text-xs-center>
    <v-layout>
      <v-flex xs10>
        <v-text-field label="Scorecard Option" v-model="_scorecardMatchOption.title" />
      </v-flex>
      <v-flex xs2>
        <v-btn class="mx-0 mt-3" @click="removeScorecardMatchOptionObject">Delete</v-btn>
      </v-flex>
    </v-layout>
  </v-container>
  <v-radio v-else label="sd" value="_scorecardMatchOption.title"></v-radio>
</template>

<script>
export default {
  name: "ScorecardMatchOption",
  props: ["scorecardMatchOption", "isNew", "isModify"],
  computed: {
    _scorecardMatchOption: {
      get: function() {
        console.log("getter called with scorecardMatchOption");
        return this.scorecardMatchOption;
      },
      set: function(scorecardMatchOption) {
        console.log("setter called with scorecardMatchOption");
        this.$emit(
          "update-scorecard-match-option",
          scorecardMatchOption,
          this.$vnode.key
        );
      }
    }
  },
  methods: {
    removeScorecardMatchOptionObject() {
      this.$delete(this._scorecardMatchOption);
    }
  }
};
</script>

<style scoped>
.centered3 {
  border: 2px solid blue;
  border-radius: 25px;
  height: 10%;
  width: 10%;
}
</style>
