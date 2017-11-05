<template>
  <v-container class="child">
    <!-- <v-container class="pa-0 pb-2">
      <v-layout row>
        <v-flex fluid grid-list-md class="grey lighten-4 " id="account-overview" xs12>
          <v-card >
            <v-card-title primary-title>
              <v-layout>
                <v-flex row>
                  <v-avatar size="80px">
                    <img :src="`https://randomuser.me/api/portraits/men/${Date.now()%13}.jpg`" alt="avatar">
                  </v-avatar>
                </v-flex>
                <v-flex>
                  <div class="headline text-xs-center">Account Overview</div>
                </v-flex>
              </v-layout>
            </v-card-title>
            <v-layout col>
              <v-flex fluid grid-list-md class="grey lighten-4" id="account-overview" xs12>
                <v-card-text class="subheading">Balance: ${{balance}}</v-card-text>
                <v-card-text class="subheading pt-0">Credit Remaining: {{creditLimit}}</v-card-text>         
                <v-card-text class="subheading pt-0">Credit Score: {{creditScore}}</v-card-text>                     
              </v-flex>
            </v-layout>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container> -->
    
    <AccountOverview :user="user"></AccountOverview>
    <v-layout row>
      <v-flex fluid grid-list-md class="grey lighten-4 " id="account-overview" xs12>
        <v-card >
          <TaskList></TaskList>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>

import TaskList from './TaskList';
import AccountOverview from './AccountOverview';

export default {
  name: 'Child',
  components: {
    TaskList,
    AccountOverview
  },
  props: ['user'],
  data () {
    return {
      name: 'Charlie Brown',
      creditLimit: 500,
      creditScore: 650,
      balance: 145
    }
  },
  methods: {
    creditRemaining(user){
      return user.virtualCreditLimit - user.virtualBalance;
    },
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
