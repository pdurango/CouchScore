<template>
  <v-container grid-list-xl text-xs-center>
    <v-layout row wrap>
      <v-flex xs10 offset-xs1>
        <Scorecard
          v-bind:scorecard="scorecard"
          v-bind:isEditable="isEditable"
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
      scorecard: { createdBy: localStorage.getItem("authToken") },
      isEditable: Boolean,
      id: Number
    };
  },
  methods: {
    saveScorecard(newScorecard) {
      //var id = newScorecard;
      this.$api
        .post("/scorecards", newScorecard)
        .then(res => (this.id = res.data.id))
        .catch(err => console.log(err));
    }
  },
  created() {
    this.isEditable = true;
  }
};
</script>

<style scoped></style>
