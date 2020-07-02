<template>
   <v-container grid-list-xl text-xs-center>
      <v-layout row wrap>
         <v-flex xs10 offset-xs1>
            <v-card color="#bdb9b9">
               <v-card-title>
                  <v-text-field label="Scorecard Match Name" v-model="scorecardMatch.title"></v-text-field>
               </v-card-title>
               <v-card-text>
                  <v-btn class="primary mx-0 mt-3" @click="addScorecardMatchOptionObject">Add an option</v-btn>

                  <div v-if="isNewScorecard">
                     <ScorecardMatchOption
                        v-bind:key="matchOptions.id"
                        v-for="matchOptions in scorecardMatch.scorecardMatchOptions"
                        v-bind:scorecardMatchOption="matchOptions"
                        v-bind:isNewScorecard="true"
                     />
                  </div>
                  <v-radio-group v-else v-model="scorecardMatch.userSelection" :mandatory="false">
                     <ScorecardMatchOption
                        v-bind:key="matchOptions.id"
                        v-for="matchOptions in scorecardMatch.scorecardMatchOptions"
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
      ScorecardMatchOption,
   },
   props: ["scorecardMatch", "isNewScorecard"],
   methods: {
      addScorecardMatchOptionArray() {
         this.$set(this.scorecardMatch, "scorecardMatchOptions", []);
      },
      addScorecardMatchOptionObject() {
         const option = {
            title: "",
            userSelection: "",
         };
         var count = this.scorecardMatch.scorecardMatchOptions.length;
         this.$set(this.scorecardMatch.scorecardMatchOptions, count, option);
      },
   },
   created() {
      if (this.isNewScorecard) this.addScorecardMatchOptionArray();
   },
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
