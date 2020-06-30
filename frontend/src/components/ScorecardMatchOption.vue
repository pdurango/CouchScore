<template>
    <div>
        <p>{{ scorecardMatchOption.name }}</p>
    </div>
</template>

<script>
export default {
    name: "ScorecardMatchOption",
    props: ["scorecardMatchOption", "isNewScorecard"]
}
</script>

<style scoped>

</style>