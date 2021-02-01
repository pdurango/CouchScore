<template>
  <v-main>
    <v-dialog v-model="dialog" persistent max-width="600px" min-width="360px">
      <div>
        <v-tabs
          v-model="tab"
          show-arrows
          background-color="deep-purple accent-4"
          icons-and-text
          dark
          grow
        >
          <v-tabs-slider color="purple darken-4"></v-tabs-slider>
          <v-tab v-for="i in tabs" :key="i.idx">
            <v-icon large>{{ i.icon }}</v-icon>
            <div class="caption py-1">{{ i.name }}</div>
          </v-tab>
          <v-tab-item>
            <v-card class="px-4">
              <v-card-text>
                <v-form ref="loginForm" v-model="validLogin" lazy-validation>
                  <v-row>
                    <v-col cols="12">
                      <v-text-field
                        v-model="loginUserName"
                        :rules="[rules.required, rules.min]"
                        label="User Name"
                        required
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12">
                      <v-text-field
                        v-model="loginPassword"
                        :append-icon="show1 ? 'eye' : 'eye-off'"
                        :rules="[rules.required, rules.min]"
                        :type="show1 ? 'text' : 'password'"
                        name="input-10-1"
                        label="Password"
                        hint="At least 8 characters"
                        counter
                        @click:append="show1 = !show1"
                      ></v-text-field>
                    </v-col>
                    <v-col class="d-flex" cols="12" sm="6" xsm="12"></v-col>
                    <v-spacer></v-spacer>
                    <v-container>
                      <v-row no-gutters>
                        <v-col cols="8">
                          <p v-if="errorMessageLogin.length > 0">{{ errorMessageLogin }}</p>
                        </v-col>
                        <v-col cols="4">
                          <v-btn
                            x-large
                            block
                            :disabled="!validLogin"
                            color="success"
                            @click="validateLogin"
                          >Login</v-btn>
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-row>
                </v-form>
              </v-card-text>
            </v-card>
          </v-tab-item>
          <v-tab-item>
            <v-card class="px-4">
              <v-card-text>
                <v-form ref="registerForm" v-model="validRegister" lazy-validation>
                  <v-row>
                    <v-col cols="12" sm="6" md="6">
                      <v-text-field
                        v-model="firstName"
                        :rules="[rules.required]"
                        label="First Name"
                        maxlength="20"
                        required
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12" sm="6" md="6">
                      <v-text-field
                        v-model="lastName"
                        :rules="[rules.required]"
                        label="Last Name"
                        maxlength="20"
                        required
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12">
                      <v-text-field
                        v-model="registerUserName"
                        :rules="[rules.required, rules.min]"
                        label="User Name"
                        required
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12">
                      <v-text-field v-model="email" :rules="emailRules" label="E-mail" required></v-text-field>
                    </v-col>
                    <v-col cols="12">
                      <v-text-field
                        v-model="registerPassword"
                        :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                        :rules="[rules.required, rules.min]"
                        :type="show1 ? 'text' : 'password'"
                        name="input-10-1"
                        label="Password"
                        hint="At least 8 characters"
                        counter
                        @click:append="show1 = !show1"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12">
                      <v-text-field
                        block
                        v-model="verify"
                        :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                        :rules="[rules.required, passwordMatch]"
                        :type="show1 ? 'text' : 'password'"
                        name="input-10-1"
                        label="Confirm Password"
                        counter
                        @click:append="show1 = !show1"
                      ></v-text-field>
                    </v-col>
                    <v-spacer></v-spacer>
                    <v-container>
                      <v-row no-gutters>
                        <v-col cols="8">
                          <p v-if="errorMessageRegister.length > 0">{{ errorMessageRegister }}</p>
                        </v-col>
                        <v-col cols="4">
                          <v-btn
                            x-large
                            block
                            :disabled="!validRegister"
                            color="success"
                            @click="validateRegister"
                          >Register</v-btn>
                        </v-col>
                      </v-row>
                    </v-container>
                  </v-row>
                </v-form>
              </v-card-text>
            </v-card>
          </v-tab-item>
        </v-tabs>
      </div>
    </v-dialog>
  </v-main>
</template>

<script>
export default {
  name: "Login",
  computed: {
    passwordMatch() {
      return () => this.registerPassword === this.verify || "Password must match";
    }
  },
  methods: {
    validateLogin() {
      if (this.$refs.loginForm.validate()) {
        {
          this.$api
            .post("/users/authenticate/login", {
              username: this.loginUserName,
              password: this.loginPassword
            })
            .then(res => {
              localStorage.setItem("username", res.data.username);
              localStorage.setItem("userId", res.data.id);
              localStorage.setItem("authToken", res.data.token);
              this.$router.push("/");
            })
            .catch(
              err => (
                console.log(err), (this.errorMessageLogin = err.data.message)
              )
            );
        }
      }
    },
    validateRegister() {
      if (this.$refs.registerForm.validate()) {
        {
          this.$api
            .post("/users/authenticate/register", {
              username: this.registerUserName,
              password: this.registerPassword,
              email: this.email,
              firstName: this.firstName,
              lastName: this.lastName
            })
            .then(res => {
                localStorage.setItem("username", res.data.username);
                localStorage.setItem("id", res.data.id);
                localStorage.setItem("authToken", res.data.token);
                this.$router.push("/");
            })
            .catch(
              err => (
                console.log(err), (this.errorMessageRegister = err.data.message)
              )
            );
        }
      }
    },
    reset() {
      this.$refs.form.reset();
    },
    resetValidation() {
      this.$refs.form.resetValidation();
    }
  },
  data: () => ({
    dialog: true,
    tab: 0,
    tabs: [
      { name: "Login", icon: "mdi-account", idx: 0 },
      { name: "Register", icon: "mdi-account-outline", idx: 1 }
    ],
    validLogin: true,
    validRegister: true,
    errorMessageLogin: "",
    errorMessageRegister: "",
    registerUserName: "",
    firstName: "",
    lastName: "",
    email: "",
    registerPassword: "",
    verify: "",
    loginUserName: "",
    loginPassword: "",
    loginEmail: "",
    loginEmailRules: [
      v => !!v || "Required",
      v => /.+@.+\..+/.test(v) || "E-mail must be valid"
    ],
    emailRules: [
      v => !!v || "Required",
      v => /.+@.+\..+/.test(v) || "E-mail must be valid"
    ],

    show1: false,
    rules: {
      required: value => !!value || "Required.",
      min: v => (v && v.length >= 8) || "Min 8 characters"
    }
  })
};
</script>
