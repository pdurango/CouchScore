<template>
    <div class="centered2">
        <div v-bind:key="matchOptions.id" v-for="matchOptions in scorecardMatch.scorecardMatchOptions">
            <ScorecardMatchOption v-bind:scorecardMatchOption="matchOptions"/>
        </div>
    </div>
</template>

<script>
import ScorecardMatchOption from './ScorecardMatchOption.vue';
export default {
    name: "ScorecardMatch",
    components: {
        ScorecardMatchOption
    },
    props: ["scorecardMatch", "isNewScorecard"]
}
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