<template>
  <div class="parent">
    <div>
      <!-- <v-text class="display-2">Family Overview</v-text> -->
      <v-layout row wrap justify-space-around class="pa-2">
          <!-- <v-flex xs12>
            <AccountOverview :user="parent"></AccountOverview>
          </v-flex> -->
          
          <v-flex
            xs6 sm4 md3
            v-for="(child, index) in children"
            :key="index"
            class="pa-2"
          >
          <v-card :href="`#/account/${child.customerId}`" :color="colorFromCreditScore(child.virtualCreditScore)" class="pa-1" router>
            <v-layout row wrap align-center>
              <v-flex class="text-xs-center pb-2">
                <v-avatar size="65%" align-center>
                  <img :src="`https://randomuser.me/api/portraits/${child.customerId%13%2 ? 'women' : 'men'}/${child.customerId%13}.jpg`" alt="avatar">
                </v-avatar>
                <v-chip color="blue-grey darken-1" text-color="white">
                <v-avatar>
                  <v-icon>account_circle</v-icon>
                </v-avatar>
                {{child.name}}
              </v-chip>
              </v-flex>
            </v-layout>
            <v-card class="grey lighten-4 text-xs-center">
              <v-card-text class="display-2">{{child.virtualCreditScore}}</v-card-text>
              <v-card-text class="subheading pt-0">Balance: ${{child.virtualBalance}}</v-card-text>
              <v-card-text class="subheading pt-0">Credit Remaining: ${{creditRemaining(child)}}</v-card-text>
            </v-card>
          </v-card>
        </v-flex>
      </v-layout>
    </div>
  </div>
</template>

<script>
import TaskList from './TaskList'
import AccountOverview from './AccountOverview';

export default {
  name: 'Parent',
  components: {
    TaskList,
    AccountOverview
  },
  computed: {
    parent() {
      return this.$store.getters.family.filter(m => m.IsPrimary)[0];
    },
    children() {
      return this.$store.getters.family.filter(m => !m.IsPrimary);
    }
  },
  methods: {
    creditRemaining(user){
      return Math.round((user.maxCreditLimit - user.virtualBalance)*100 )/100;
    },
    colorFromCreditScore(creditScore){
      if(creditScore < 600)
        return "red lighten-1";
      if(creditScore > 800)
        return "teal accent-3";
      return "amber accent-2";
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>