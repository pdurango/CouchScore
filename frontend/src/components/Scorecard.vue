<template>
    <div class="centered">
        <v-card>
            <v-card-title>
                <h1>dfdf</h1>
            </v-card-title>
            <v-card-text>
                <v-form class="px-3">
                    <v-text-field label="Scorecard Name" v-model="scorecard.title"></v-text-field>
                    <v-btn class="success mx-0 mt-3" @click.prevent="saveScorecard">Save Scorecard</v-btn>
                    <v-btn class="primary mx-0 mt-3" @click="addScorecardMatchObject">Add a match</v-btn>
                </v-form>
            </v-card-text>
        
            <div v-if="scorecard.scorecardMatches.length > 0">
                <ScorecardMatch v-bind:key="match.id" v-for="match in scorecard.scorecardMatches" v-bind:scorecardMatch="match" v-bind:isNewScorecard="true"/>
            </div>
        </v-card>
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
        addScorecardMatchArray() {
            this.$set(this.scorecard, 'scorecardMatches', []);
            //var updated = this.scorecard;
           // updated.push({"element":{ id: 1, quantity: 1 }});
           // this.$emit('update-local-scorecard', updated);
        },
        addScorecardMatchObject() {
            const match = {
                title: ''
            }
            var count = this.scorecard.scorecardMatches.length;
            this.$set(this.scorecard.scorecardMatches, count, match);
        },
        saveScorecard() {
            this.$emit('save-scorecard', this.scorecard);
        }
    },
    created()
    {
        if(this.isNewScorecard)
            this.addScorecardMatchArray();
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
        border: 2px solid green;
        padding: 10px;
        border-radius: 25px;
        position: fixed;
        height: 80%;
        width: 50%;
        top: 50%;
        left: 50%;
        /* bring your own prefixes */
        transform: translate(-50%, -50%);
    }
</style>