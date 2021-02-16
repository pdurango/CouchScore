<template>
        <v-dialog v-model="dialog"
        persistent
        max-width="290"
        >
        <template v-slot:activator="{on, attrs}">
            <v-btn color="primary mx-0 mt-3" v-bind="attrs" v-on="on">
                Invite Users
            </v-btn>
        </template>
        <v-card>
            <v-card-title class="headline">Invite Users</v-card-title>
            <v-card-text>
                <v-container>
                    <v-row
                    v-for="(username, index) in usernames"
                    v-bind:key="index"
                    >
                        <v-col cols="12" sm="12" md="12">
                                <v-text-field
                                v-model="usernames[index]"
                                label="Username"
                                persistent-hint
                                required
                                >
                            </v-text-field>
                        </v-col>
                    </v-row>
                </v-container>
                <!--<small>* indicates brown bricks in minecraft</small>-->
            </v-card-text>       
            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="blue darken-1" text @click="addEmptyUserEntry">Add User</v-btn>
                <v-btn color="blue darken-1" text @click="closeDialog">Close</v-btn>
                <v-btn color="blue darken-1" text @click="inviteUsers">Save</v-btn>
            </v-card-actions>     
        </v-card>
        </v-dialog>
</template>

<script>
export default {
    name: "InviteUsers",
    props: ["scorecardId"],
    data() {
        return {
        dialog: false,
        usernames: [],
        invalidUsernames: []
        };
    },
    methods: 
    {
        inviteUsers()
        {
            this.$api
            .put("/scorecards/inviteUsers/" + this.scorecardId, this.usernames)
            .then(
                res => 
                {
                this.usernames = [];
                console.log(res.data); //todo - deleteme
                this.dialog = false;
                
                /*
                Not sure if i'm going to support displaying invalid users so ignore for now
                this.invalidUsernames = res.data;
                if(this.invalidUsernames.length == 0)
                    this.dialog = false;
                */
                //this.$emit("save-scorecard", this.currentScorecard);
                }
            )
          .catch(err => console.log(err));
        },
        addEmptyUserEntry()
        {
            this.$set(this.usernames, this.usernames.length, "");
        },
        closeDialog()
        {
            this.usernames = [];
            this.dialog = false;
        }
    }
}
</script>

<style scoped>

</style>