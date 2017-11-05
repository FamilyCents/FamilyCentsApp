<template>
  <v-container class="child">
    <v-container class="pa-0 pb-2">
      <v-layout row>
        <v-flex fluid grid-list-md class="grey lighten-4 " id="account-overview" xs12>
          <v-card >
            <v-card-title primary-title>
              <div class="headline">Account Overview</div>
            </v-card-title>
            <v-layout col>
              <v-flex fluid grid-list-md class="grey lighten-4" id="account-overview" xs12>
                <v-card-text class="subheading">Balance: ${{balance}}</v-card-text>
                <v-card-text class="subheading pt-0">Credit Limit: {{creditLimit}}</v-card-text>         
                <v-card-text class="subheading pt-0">Credit Score: {{creditScore}}</v-card-text>                     
              </v-flex>
            </v-layout>
          </v-card>
        </v-flex>
      </v-layout>
    </v-container>
    
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

export default {
  name: 'Child',
  components: {
    TaskList
  },
  data () {
    return {
      name: 'Charlie Brown',
      creditLimit: 500,
      creditScore: 650,
      balance: 145
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
