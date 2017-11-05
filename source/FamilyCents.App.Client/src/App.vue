<template>
  <v-app>
    <!-- <v-navigation-drawer app></v-navigation-drawer> -->
    <v-toolbar app></v-toolbar>
    <main>
      <v-content>
        <router-view>
          <v-container fluid></v-container>
        </router-view>
      </v-content>
    </main>
    <v-footer app></v-footer>
  </v-app>
</template>

<script>
// Load correct component e.g. Child, Parent

export default {
  name: 'app'
}
</script>

<style lang="scss">
</style>

