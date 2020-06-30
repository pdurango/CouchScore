<template>
    <div class="centered">
        <form @submit.prevent="updateScorecard">
            <input type="text" v-model="scorecard.title" name="title" placeholder="Add Scorecard Name...">
            <input type="submit" value="Submit" class="btn">
        </form>
        <div v-bind:key="match.id" v-for="match in scorecard.scorecardMatches">
            <ScorecardMatch v-bind:scorecardMatch="match"/>
        </div>
        <button v-on:click="addScorecardMatchRow">Add Match</button>
    </div>
</template>

<script>
import ScorecardMatch from './ScorecardMatch.vue';
export default {
    name: "Scorecard",
    components: {
        ScorecardMatch
        },
    props: ["scorecard", "isNewScorecard"],
    methods: {
        updateScorecard() {
            const newScorecard = {
                title: this.title //"this" is the form element, which contains "title"
            }
            //Send up to parent
            this.$emit('update-local-scorecard', newScorecard);
        },
        addScorecardMatchRow() {
            this.$set(this.scorecard, 0, [{"ScorecardMatches":{ id: 1, quantity: 1 }}]);
            //var updated = this.scorecard;
           // updated.push({"element":{ id: 1, quantity: 1 }});
           // this.$emit('update-local-scorecard', updated);
        }
    },
    created() {
        if(!this.isNewScorecard)
            return;
        const modifed = {
            "Scorecard":{ id: 1, quantity: 1 }
            }
        this.$emit('update-local-scorecard', modifed);

    }
}
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
        border: 2px solid black;
        padding: 10px;
        border-radius: 25px;
        position: fixed;
        height: 30%;
        width: 50%;
        top: 50%;
        left: 50%;
        /* bring your own prefixes */
        transform: translate(-50%, -50%);
    }
</style>