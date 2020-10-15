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
import axios from "axios";

export default {
   name: "Create",
   components: {
      Scorecard,
   },
   data() {
      return {
         scorecard: { },
         isEditable: Boolean,
      };
   },
   methods: {
      saveScorecard(newScorecard) {
         var id = newScorecard;
         axios
            .post("/scorecards", newScorecard)
            .then((res) => (id = res.id))
            .catch((err) => console.log(err));

         console.log(id);
      },
   },
   created() {
      this.isEditable = true;
   },
};
</script>

<style scoped></style>
