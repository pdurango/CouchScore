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
        Scorecard
    },
    data() {
        return {
            scorecard: {    
                title: ''
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