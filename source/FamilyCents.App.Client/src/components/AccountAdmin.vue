<template>
  <div class="parent">
    <AccountOverview :user="user" v-if="user"></AccountOverview>
    <v-progress-linear v-bind:indeterminate="true" v-else></v-progress-linear>
    <v-layout row wrap>
      <v-flex xs12 class="pt-2 ml-2">
        <div>
          <v-icon>settings</v-icon>
        </div>
      </v-flex>
      <v-flex class="pa-2" xs6>
        <v-text-field label="Minimum Credit Limit" v-model="minCredit" type="number"></v-text-field>
      </v-flex>
      <v-flex class="pa-2" xs6>
        <v-text-field label="Maximum Credit Limit" v-model="maxCredit" type="number"></v-text-field>
      </v-flex>
      <v-flex>
        <div class="text-xs-center">
          <v-btn round color="cyan" dark @click="updateCreditRange">Update Settings</v-btn>
        </div>
      </v-flex>
    </v-layout>
    <!-- <TaskList></TaskList> -->
  </div>
</template>

<script>
import AccountOverview from './AccountOverview';

export default {
  name: 'AccountAdmin',
  components: {
    AccountOverview
  },
  data () {
    return {
      minCredit: 0,
      maxCredit: 0
    }
  },
  computed: {
    user: function(){
      return this.$store.getters.family.filter(m => m.customerId == this.$route.params.id)[0];
    }
  },
  methods: {
    updateCreditRange: function(){
      this.$store.dispatch('updateUser', [this.$route.params.id, {minCreditLimit: this.minCredit, maxCreditLimit: this.maxCredit}]);
    }
  },
  mounted(){
    this.$store.dispatch('getUserPromise', this.$route.params.id).then(u => {
      this.minCredit = u.minCreditLimit;
      this.maxCredit = u.maxCreditLimit; 
    });
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>