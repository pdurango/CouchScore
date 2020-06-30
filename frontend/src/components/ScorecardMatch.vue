<template>
    <div>
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

</style>