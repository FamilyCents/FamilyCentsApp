<template>
  <div class="parent">
    <div>
      <!-- <v-text class="display-2">Family Overview</v-text> -->
      <v-layout row wrap justify-space-around class="pa-2">
          <v-flex
            xs6 sm4 md3
            v-for="(child, index) in children"
            :key="index"
            class="pa-2"
          >
          <v-card :href="`/account/${child.customerId}`" :color="colorFromCreditScore(child.virtualCreditScore)" class="pa-1" router>
            <v-layout row wrap align-center>
              <v-flex class="text-xs-center pb-2">
                <v-avatar size="65%" align-center>
                  <img :src="`https://randomuser.me/api/portraits/men/${child.customerId%13}.jpg`" alt="avatar">
                </v-avatar>
              </v-flex>
            </v-layout>
            <v-card class="grey lighten-4">
              <v-card-text class="subheading">Balance: {{child.virtualBalance}}</v-card-text>
              <v-card-text class="subheading pt-0">Credit Remaining: {{creditRemaining(child)}}</v-card-text>
              <v-card-text class="subheading pt-0">Credit Score: {{child.virtualCreditScore}}</v-card-text>
            </v-card>
          </v-card>
        </v-flex>
      </v-layout>
    </div>
  </div>
</template>

<script>
import TaskList from './TaskList'
export default {
  name: 'Parent',
  components: {
    TaskList
  },
  computed: {
    children() {
      return this.$store.getters.family.filter(m => !m.IsPrimary);
    }
  },
  methods: {
    creditRemaining(user){
      return user.virtualCreditLimit;// - user.virtualBalance;
    },
    colorFromCreditScore(creditScore){
      if(creditScore < 500)
        return "red";
      if(creditScore > 650)
        return "green";
      return "yellow";
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>