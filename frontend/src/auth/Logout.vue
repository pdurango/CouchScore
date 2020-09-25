<template>
  <div></div>
</template>

<script>
export default {
  name: "Logout",
  created() {
    localStorage.removeItem("authToken");
    this.$router.push("/login");
  }
};
</script>