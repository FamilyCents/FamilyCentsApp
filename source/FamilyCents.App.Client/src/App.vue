<template>
  <v-app id="inspire">
    <v-navigation-drawer
      temporary
      v-model="drawer"
      enable-resize-watcher
      right
      app
      width="175"
    >
      <v-list dense>
        <v-list-tile href="/" router>
          <v-list-tile-action>
            <v-icon>home</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Home</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
        <v-list-tile href="#/tasks" router>
          <v-list-tile-action>
            <v-icon>list</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Tasks</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar color="cyan" dark fixed app>
      <v-toolbar-title>Family Cents</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
    </v-toolbar>
    <main>
      <v-content>
        <router-view>
          <v-container fluid fill-height></v-container>
        </router-view>
      </v-content>
    </main>
    <!-- <v-footer color="cyan" app>
      <v-spacer></v-spacer>
      <span class="white--text">&copy; 2017</span>
    </v-footer> -->
  </v-app>
</template>

<script>
// Load correct component e.g. Child, Parent

export default {
  name: 'app',
  data: () => ({
      drawer: false
  }),
  beforeCreate(){
    console.log(this.$route.query.isChild);
    let userId = !this.$route.query.isChild ? 152310000 : 152320000;
    console.log(userId);
    this.$store.dispatch('initializeFamily', [152300000, userId]);
  },
  created() {
    // setInterval(
    //   () => this.$store.dispatch('reloadFamily')
    //   , 4000);
  }
}
</script>

<style lang="scss">
</style>

