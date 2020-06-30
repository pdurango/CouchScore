<template>
    <div class="centered3">
        <p>{{ scorecardMatchOption.title }}</p>
    </div>
</template>

<script>
export default {
    name: "ScorecardMatchOption",
    props: ["scorecardMatchOption", "isNewScorecard"]
}
</script>

<style scoped>
    .centered3 {
        border: 2px solid blue;
        border-radius: 25px;
        height: 10%;
        width: 10%;
    }
</style>