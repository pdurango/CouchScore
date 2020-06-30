<template>
    <div id="Create">
        <p>ahhh</p>
        <Scorecard v-bind:scorecard="scorecard" v-bind:isNewScorecard="isNewScorecard" v-on:save-scorecard="saveScorecard"/>
    </div>    
</template>

<script>
import Scorecard from '../components/Scorecard.vue'
import axios from 'axios'

export default {
    name: "Create",
    components: {
        Scorecard
    },
    data() {
        return {
            scorecard: { /*
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
           */ },
           isNewScorecard: Boolean
        }
    },
    methods: {
        saveScorecard(newScorecard)
        {
            var id;
            axios.post(process.env.VUE_API_URL + '/scorecards', newScorecard)
            .then(res => id = res.id )
            .catch(err => console.log(err));
            
            console.log(id);
        }
    },
    created()
    {
        this.isNewScorecard = true;
    }
}
</script>

<style scoped>

</style>