<template>
    <div class="centered2">
                <v-btn color="green" @click="addScorecardMatchOptionObject">Add an option</v-btn>
        <div v-if="scorecardMatch.scorecardMatchOptions.length > 0">
            <ScorecardMatchOption v-bind:key="matchOptions.id" v-for="matchOptions in scorecardMatch.scorecardMatchOptions" v-bind:scorecardMatchOption="matchOptions"/>
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
    props: ["scorecardMatch", "isNewScorecard"],
    methods: {
        addScorecardMatchOptionArray() {
            this.$set(this.scorecardMatch, 'scorecardMatchOptions', []);
        },
        addScorecardMatchOptionObject() {
            const option = {
                title: ''
            }
            var count = this.scorecardMatch.scorecardMatchOptions.length;
            this.$set(this.scorecardMatch.scorecardMatchOptions, count, option);
        }   
    },
    created()
    {
        if(this.isNewScorecard)
            this.addScorecardMatchOptionArray();
    }
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