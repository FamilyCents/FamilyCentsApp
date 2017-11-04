<template>
    <header class="main-header">
        <!-- Logo -->
        <router-link to="/dashboard" class="logo">
            <!-- mini logo for sidebar mini 50x50 pixels --><span class="logo-mini"><b> <img src="/static/img/fclogo.png" alt="User Image"></b></span>
            <!-- logo for regular state and mobile devices --><span class="logo-lg"><img src="/static/img/fclogo.png" alt="User Image"><b>Family</b>Cents</span> </router-link>
        <!-- Header Navbar: style can be found in header.less -->
        <nav class="navbar navbar-static-top">
            <!-- Sidebar toggle button-->
            <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button"> <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span> <span class="icon-bar"></span> <span class="icon-bar"></span> </a>
                                <div class="pull-left-search">
                        <form id="demo-2">
	<input class="fc-search" type="search">
</form>    
    </div>
            <div class="navbar-custom-menu">
                <ul class="nav navbar-nav">
                    <!-- Control Sidebar Toggle Button -->
                    <li>
                        <el-dropdown class="header-dropdown-familycents" trigger="click"> <span class="el-dropdown-link">
                            
                            <el-button data-toggle="control-sidebar" type="text" size="small"><i class="dripicons-swap"></i></el-button>
                          
                          </span> </el-dropdown>
                    </li>
                  
                </ul>
            </div>
        </nav>
    </header>
</template>
<script>
  export default {
    name: 'DashboardHeader',
    mounted: function () {

    }
  }
</script>