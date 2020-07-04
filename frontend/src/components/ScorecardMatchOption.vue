<template>
   <v-container v-if="isEditable" grid-list-xl text-xs-center>
      <v-layout>
         <v-flex xs10>
            <v-text-field label="Scorecard Option" v-model="scorecardMatchOption.title" />
         </v-flex>
         <v-flex xs2> <v-btn>Delete</v-btn> </v-flex>
      </v-layout>
   </v-container>

   <v-radio v-else label="sd" value="scorecardMatchOption.title"> </v-radio>
</template>

<script>
export default {
   name: "ScorecardMatchOption",
   props: ["scorecardMatchOption", "isEditable"],
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
