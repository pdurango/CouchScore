<template>
    <div id="Create">
        <p>ahhh</p>
        <Scorecard v-bind:scorecard="scorecard" v-bind:isNewScorecard="true" v-on:update-local-scorecard="updateLocalScorecard"/>
    </div>    
</template>

<script>
import Scorecard from '../components/Scorecard.vue'
//import ScorecardModel from '../models/ScorecardModel.ts'
import axios from 'axios'

export default {
    name: "Create",
    components: {
        Scorecard,
        //ScorecardModel
    },
    data() {
        return {
            scorecard: {
    "id": "LaCQ6lGp0c7LQuR5rMA",
    "createdDate": "2020-06-29",
    "title": '4',
    "scorecardLinkedUsers": null,
    "scorecardMatches": [
        {
            "id": 6,
            "created": "0001-01-01",
            "title": '3',
            "scorecardMatchOptions": [
                {
                    "id": 11,
                    "title": '1',
                    "scorecardUserSelections": null
                },
                {
                    "id": 12,
                    "title": '2',
                    "scorecardUserSelections": null
                }
            ]
        }
    ]
}
        }
    },
    methods: {
        createScorecard(newScorecard) 
        {
        axios.post(process.env.VUE_API_URL, newScorecard)
        //.then(res => this.todos = [...this.todos, res.data] )
        .catch(err => console.log(err));
        },
        updateLocalScorecard(updatedScorecard)
        {
            this.scorecard = updatedScorecard;
        }
    }
}
</script>

<style scoped>

</style>