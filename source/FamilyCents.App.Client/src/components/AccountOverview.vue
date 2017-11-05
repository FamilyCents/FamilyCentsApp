<template>
  <v-container class="pa-0 pb-2">
    <v-layout row>
      <v-flex fluid grid-list-md class="grey lighten-4 " id="account-overview" xs12>
        <v-card >
          <v-card-title primary-title>
            <v-layout>
              <v-flex row >
                <v-flex class="text-xs-center pb-0">
                <v-avatar size="80px" align-center>
                  <img :src="`https://randomuser.me/api/portraits/${(user ? user.customerId%13%2 : 0) ? 'women' : 'men'}/${(user ? user.customerId%13 : 0) }.jpg`" alt="avatar">
                </v-avatar>
                <p>{{user.name}}</p>
                </v-flex>
              </v-flex>
              <v-flex>
                <div class="headline text-xs-center">Account Overview</div>
                <v-flex class="text-xs-center pb-2">
                <v-avatar size="80%" align-center>
                  <img :src="creditGauge(user.virtualCreditScore)" alt="creditguage">
                </v-avatar>
                  <v-card-text class="subheading pt-0">Credit Score: {{user.virtualCreditScore}}</v-card-text>                     
              </v-flex>
                <img  width="60%"> </img >
              </v-flex>
            </v-layout>
          </v-card-title>
          <v-layout col>
            <v-flex fluid grid-list-md class="grey lighten-4" id="account-overview" xs12>
              <v-card-text class="subheading">Balance: {{user.virtualBalance.toLocaleString("en-US", { style:"currency", currency:"USD" })}}</v-card-text>
              <v-card-text class="subheading pt-0">Credit Remaining: {{creditRemaining(user).toLocaleString("en-US", { style:"currency", currency:"USD" })}}</v-card-text>         
            </v-flex>
          </v-layout>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>

export default {
  name: 'AccountOverview',
  props: ['user'],
  methods: {
    creditRemaining(user){
      return Math.round((user.maxCreditLimit - user.virtualBalance)*100 )/100;
    },
    creditGauge(creditScore){
      if(creditScore < 600)
        return "/static/30.png";
      if(creditScore > 800)
        return "/static/70.png";
      return "/static/50.png";
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
